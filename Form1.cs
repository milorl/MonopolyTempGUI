using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;

namespace MonopolyTempGUI
{
    public partial class Form1 : Form
    {
        HubConnection connection;

        Gameboard gameboard = new Gameboard();

        public Form1()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder()
           .WithUrl("https://localhost:5002/monopolyHub")
           .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        private void handlers()
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.Invoke((Action)(() =>
                {
                    var newMessage = $"{user}: {message}\n";
                    //DisplayMessage(newMessage);
                    chatBox.AppendText(newMessage);
                }));
            });

            connection.On<string, string, int, string>("ReceiveRoomInfo", (roomname, user, playerid, message) =>
            {
                this.Invoke((Action)(() =>
                {
                    if (message.Equals("join"))
                    {
                        Player newPlayer = new Player(user, gameboard.startingBalance(), 1);
                        gameboard.joinPlayer(newPlayer, playerid);
                        gameboard.setRoomname(roomname);

                        labelRoom.Visible = textBoxRoom.Visible = buttonJoin.Visible = buttonCreate.Visible = false;
                        textBox2.Visible = textBoxMessage.Visible = chatBox.Visible = actionBox.Visible = walletBox.Visible = positionBox.Visible = sellBox1.Visible = sellBox2.Visible = true;
                        labelChat.Visible = labelLog.Visible = labelWallet.Visible = labelPositions.Visible = true;
                        buttonSend.Visible = buttonMove.Visible = buttonEnd.Visible = buttonSell.Visible = buttonBuy.Visible = true;

                        textBox2.Text = roomname;

                        if (playerid == 0)
                        {
                            gameboard.setActivePlayer(user);
                            buttonMove.Enabled = sellBox1.Enabled = sellBox2.Enabled = buttonSell.Enabled = true;
                        }
                    }
                    else
                    {
                        gameboard.removePlayer(user);
                    }
                }));
            });

            connection.On<List<string>>("ReceiveRoomMembers", (userlist) =>
            {
                this.Invoke((Action)(() =>
                {
                    Player p = new Player();
                    for(int i = 0; i<userlist.Count; i++)
                    {
                        p = new Player(userlist[i], gameboard.startingBalance(), 1);
                        gameboard.joinPlayer(p, i);
                    }
                    int j = 0;
                    positionBox.Clear();
                    foreach (Player x in gameboard.getPlayers())
                    {
                        positionBox.AppendText("Player" + j + ": " + x.getUsername() + "\n");
                        j++;
                    }
                }));
            });

            connection.On<string, int, int>("ReceiveMoveInfo", (user, roll_1, roll_2) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.makeMove(user, roll_1 + roll_2);

                    var newMessage = $"{user}: ruszył się o {roll_1+roll_2}\n";
                    actionBox.AppendText(newMessage);
                    
                    if(user.Equals(gameboard.getUsername()))
                    {
                        buttonMove.Enabled = false;
                        Field f = gameboard.getField(gameboard.getPlayerByUsername(user).getCurrentField());
                        string CheckOwner = f.getOwner();
                        if (CheckOwner.Equals("none")) buttonBuy.Enabled = true;
                        else if (!CheckOwner.Equals(user)) payTax(user, f.getOwner(), f.getTaxVal());
                        buttonEnd.Enabled = true;
                    }
                }));
            });

            connection.On<string, int>("ReceivePurchaseInfo", (user, field) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.buyField(user, field);

                    var newMessage = $"{user}: kupił pole nr {field} \n";
                    actionBox.AppendText(newMessage);

                    updateWallets();
                }));
            });

            connection.On<string, int, int>("ReceivePurchaseInfo", (user, field, price) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.buyField(user, field, price);

                    var newMessage = $"{user}: kupił pole nr {field} \n";
                    actionBox.AppendText(newMessage);

                    updateWallets();

                    buttonCreate.Visible = false;
                }));
            });

            connection.On<string, int>("ReceiveWalletBalance", (user, balance) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.setBalance(user, balance);

                    var newMessage = $"{user}: zmienił stan konta na: {balance} \n";
                    actionBox.AppendText(newMessage);
                }));
            });

            connection.On<int>("ReceiveAuctionInfo", (field) =>
            {
                this.Invoke((Action)(() =>
                {
                    var newMessage = $"Pole: {field} wystawione na sprzedaz \n";
                    actionBox.AppendText(newMessage);
                    auctionBox.Visible = buttonOffer.Visible = buttonAbort.Visible = true;
                }));
            });

            connection.On<string, string, int>("ReceiveTaxInfo", (user, owner, price) =>
            {
                this.Invoke((Action)(() =>
                {
                    actionBox.AppendText("Uzytkownik " + user + " placi " + price + " uzytkownikowi " + owner + "\n");
                    gameboard.pay(user, owner, price);

                    updateWallets();
                }));
            });

            connection.On<string>("ReceiveEndTurn", (user) =>
            {
                this.Invoke((Action)(() =>
                {
                    actionBox.AppendText("Uzytkownik " + user + " konczy ture\n");
                    gameboard.nextTurn(user);
                    if(gameboard.getActivePlayer().Equals(gameboard.getUsername()))
                    {
                        buttonMove.Enabled = true;
                    }
                    else buttonBuy.Enabled = buttonEnd.Enabled = sellBox1.Enabled = sellBox2.Enabled = buttonSell.Enabled = false;

                    actionBox.AppendText("Tura gracza " + gameboard.getActivePlayer() + " \n");

                    updateWallets();
                }));
            });

            connection.On<string>("RoomExistsErr", (roomname) =>
            {
                this.Invoke((Action)(() =>
                {
                    //display error
                    textBoxRoom.Text = "Room already exists!";
                }));
            });

            connection.On<string>("RoomNotExistsErr", (roomname) =>
            {
                this.Invoke((Action)(() =>
                {
                    //display error
                    textBoxRoom.Text = "Room does not exist!";
                }));
            });

            connection.On<string>("ReceiveResign", (user) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.resign(user);
                }));
            });

            connection.On<string, int, int>("ReceiveWinner", (user, price, id) =>
            {
                this.Invoke((Action)(() =>
                {
                    if(gameboard.getUsername().Equals(user))
                    {
                        System.Windows.Forms.MessageBox.Show("Wygrales licytacje pola!");
                    }

                    gameboard.buyField(user, id, price);
                }));
            });

            connection.On<string, string>("ReceiveAcc", (acc, username) =>
            {
                this.Invoke((Action)(() =>
                {
                    if (acc.Equals("ok"))
                    {
                        gameboard.setUsername(username);
                        textBox1.Text = username;
                        textBox1.Visible = textBoxRoom.Visible = buttonJoin.Visible = buttonCreate.Visible = true;
                        labelPassword.Visible = labelLogin.Visible = textBoxLogin.Visible = textBoxPassword.Visible = buttonRegister.Visible = buttonLogin.Visible = false;
                    }
                    //else display error
                }));
            });

        }
        public class Player
        {
            string username;
            int walletBalance;
            int state;              // NOT_ACTIVE = 0, FREE = 1, PRISON = 2 
            bool debt;
            int currentField;

            public Player()
            {
                username = ""; walletBalance = 0; state = 0; currentField = 0; debt = false;
            }

            public Player(string username, int walletBalance, int state)
            {
                this.username = username; this.walletBalance = walletBalance; this.state = state; 
                currentField = 0; debt = false;
            }

            public void setPlayerstate(int state)
            {
                this.state = state;
            }

            public void pay(int price)
            {
                walletBalance = walletBalance - price;
            }

            public void addcash(int val)
            {
                walletBalance = walletBalance + val;
            }

            public void updateWallet(int balance)
            {
                walletBalance = balance;
            }

            public int getBalance()
            {
                return walletBalance;
            }

            public string getUsername()
            {
                return username;
            }

            public int CheckState()
            {
                return state;
            }

            public void changeCurrentField(int range)
            {
                currentField = currentField + range;
            }

            public int getCurrentField()
            {
                return currentField;
            }
            public int getPos()
            {
                return currentField;
            }

            public static bool operator ==(Player p1, Player p2)
            {
                return p1.getUsername().Equals(p2.getUsername());
            }

            public static bool operator !=(Player p1, Player p2) => !(p1 == p2);
        }

        public class Field
        {
            int price;
            int tax;
            int position;
            string owner;
            int special; // DEFAULT = 0, START = 1, PRISON = 2, BANK = 3, TAX = 4
            List<Player> presentPlayers;

            public Field(int position, int price, int tax)
            {
                this.position = position; this.price = price; this.tax = tax;
                owner = "none"; special = 0; presentPlayers = new List<Player>();
            }

            public int getTaxVal()
            {
                return tax;
            }

            public int getPrice()
            {
                return price;
            }

            public int getPosition()
            {
                return position;
            }

            public string getOwner()
            {
                return owner;
            }

            public int CheckSpecial()
            {
                return special;
            }

            public void setOwner(string owner)
            {
                this.owner = owner;
            }

            public void addPlayer(Player p)
            {
                presentPlayers.Add(p);
            }

            public void removePlayer(Player p)
            {
                presentPlayers.Remove(p);
            }

            public static bool operator ==(Field f1, Field f2)
            {
                return (f1.getPosition() == f2.getPosition());
            }

            public static bool operator !=(Field f1, Field f2) => !(f1 == f2);

        }

        public class Gameboard
        {
            string my_roomname;
            string my_username;

            string activePlayer;
            List<Field> fields;
            List<Player> players;

            public Gameboard()
            {
                my_roomname = "";
                my_username = "";
                activePlayer = "";
                fields = new List<Field>();
                players = new List<Player>();
                Player t;
                for(int i = 0; i<4; i++)
                {
                    t = new Player("", i, 0);
                    players.Add(t);
                }

                Field temp;
                for (int i = 0; i < 28; i++)
                {
                    if (i == 0 || i == 7 || i == 14 || i == 21)
                    {
                        temp = new Field(i, 0, 0);
                        fields.Add(temp);
                    }
                    else if (i > 1 && i < 7)
                    {
                        temp = new Field(i, 100, 10);
                        fields.Add(temp);
                    }
                    else if (i > 7 && i < 14)
                    {
                        temp = new Field(i, 200, 20);
                        fields.Add(temp);
                    }
                    else if (i > 14 && i < 21)
                    {
                        temp = new Field(i, 300, 30);
                        fields.Add(temp);
                    }
                    else
                    {
                        temp = new Field(i, 400, 40);
                        fields.Add(temp);
                    }
                }
            }
            public void joinPlayer(Player newPlayer, int id)
            {
                players[id] = newPlayer;
                if (players.Count() == 1) activePlayer = newPlayer.getUsername();
            }
            public void removePlayer(Player newPlayer)
            {
                players.Remove(newPlayer);
            }

            public void removePlayer(string username)
            {
                foreach (Player p in players)
                {
                    if (p.getUsername().Equals(username)) players.Remove(p);
                }
            }

            public void nextTurn(string username)
            {
                int turnId = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (players[i].getUsername().Equals(username))
                    {
                        if (i+1<4)
                        {
                            if(players[i+1].CheckState() != 0) turnId = i + 1;
                        }
                    }
                }
                activePlayer = players[turnId].getUsername();
            }

            public Player getPlayerByUsername(string username)
            {
                Player found = new Player();
                foreach (Player p in players)
                {
                    if (p.getUsername().Equals(username))
                    {
                        found = p;
                        break;
                    }
                }
                return found;
            }

            public List<Player> getPlayers()
            {
                return players;
            }

            public int startingBalance()
            {
                //temp
                return 500;
            }

            public void makeMove(string username, int steps)
            {
                Player p = getPlayerByUsername(username);
                fields[p.getCurrentField()].removePlayer(p);
                p.changeCurrentField(steps);
                fields[p.getCurrentField()].addPlayer(p);
            }

            public void buyField(string username, int id)
            {
                fields[id].setOwner(username);
                foreach(Player p in players)
                {
                    if(p.getUsername().Equals(username))
                    {
                        p.pay(fields[id].getPrice());
                        break;
                    }
                }
            }

            public void buyField(string username, int id, int price)
            {
                fields[id].setOwner(username);
                foreach (Player p in players)
                {
                    if (p.getUsername().Equals(username))
                    {
                        p.pay(price);
                        break;
                    }
                }
            }

            public void setBalance(string username, int balance)
            {
                getPlayerByUsername(username).updateWallet(balance);
            }

            public int getBalance(string username)
            {
                return getPlayerByUsername(username).getBalance();
            }

            public void resign(string username)
            {
                removePlayer(username);
                foreach (Field field in fields)
                {
                    if (field.getOwner().Equals(username)) field.setOwner("none");
                }
            }

            public int countPlayers()
            {
                int result = 0;
                foreach(Player p in players)
                {
                    if (p.CheckState() != 0) result++;  
                }
                return result;
            }

            public void setUsername(string username)
            {
                my_username = username;
            }

            public void setRoomname(string roomname)
            {
                my_roomname = roomname;
            }

            public string getRoomname()
            {
                return my_roomname;
            }

            public string getUsername()
            {
                return my_username;
            }

            public string getActivePlayer()
            {
                return activePlayer;
            }

            public void setActivePlayer(string username)
            {
                activePlayer = username;
            }

            public int getBuyId()
            {
                return getPlayerByUsername(my_username).getPos();
            }

            public Field getField(int id)
            {
                return fields[id];
            }

            public void pay(string user, string owner, int price)
            {
                foreach (Player p in players)
                {
                    if (p.getUsername().Equals(user))
                    {
                        p.pay(price);
                    }
                    else if(p.getUsername().Equals(owner))
                    {
                        p.addcash(price);
                    }
                }
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            await connection.StartAsync();
            handlers();
            await connection.InvokeAsync("Login", textBoxLogin.Text, textBoxPassword.Text);
        }

        private async void buttonRegister_Click(object sender, EventArgs e)
        {
            await connection.StartAsync();
            await connection.InvokeAsync("Register", textBoxLogin.Text, textBoxPassword.Text);
            handlers();
        }

        private async void buttonJoin_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("JoinRoom", textBoxRoom.Text, gameboard.getUsername());
        }

        private async void buttonMove_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("SendMoveInfo", gameboard.getRoomname(), gameboard.getUsername(), 3, 2);
        }

        private async void buttonBuy_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("SendPurchaseInfo", gameboard.getRoomname(), gameboard.getUsername(), gameboard.getBuyId(), 0);
        }

        private async void buttonEnd_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("SendEndTurn", gameboard.getRoomname(), gameboard.getUsername());
            buttonBuy.Enabled = buttonEnd.Enabled = false;
        }

        private void updateWallets()
        {
            walletBox.Clear();
            foreach (Player p in gameboard.getPlayers())
            {
                if(!p.getUsername().Equals("")) walletBox.AppendText(p.getUsername() + ": " + p.getBalance() + "$\n");
            }
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("SendMessage", gameboard.getRoomname(), gameboard.getUsername(), textBoxMessage.Text);
            textBoxMessage.Clear();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("CreateRoom", textBoxRoom.Text, gameboard.getUsername());
        }

        private async void payTax(string my_name, string owner, int tax_val)
        {
            await connection.InvokeAsync("PayTax", gameboard.getRoomname(), my_name, owner, tax_val);
        }

        private async void buyField(string username, int field_id, int price)
        {
            await connection.InvokeAsync("SendPurchaseInfo", gameboard.getRoomname(), username, field_id, price);
        }

        private async void buttonSell_Click(object sender, EventArgs e)
        {
            int field_id = Convert.ToInt32(sellBox1.Text);
            if (gameboard.getField(field_id).getOwner().Equals(gameboard.getUsername())) await connection.InvokeAsync("StartAuction", gameboard.getRoomname(), Convert.ToInt32(sellBox1.Text), Convert.ToInt32(sellBox2.Text), gameboard.countPlayers()-1);
            else System.Windows.Forms.MessageBox.Show("Nie jestes wlascicielem tego pola!");
        }

        private async void buttonOffer_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("SendOffer", gameboard.getRoomname(), gameboard.getUsername(), Convert.ToInt32(auctionBox.Text));
            auctionBox.Visible = buttonOffer.Visible = buttonAbort.Visible = false;
        }

        private async void buttonAbort_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("SendAbort", gameboard.getRoomname(), gameboard.getUsername());
            auctionBox.Visible = buttonOffer.Visible = buttonAbort.Visible = false;
        }
    }
}
