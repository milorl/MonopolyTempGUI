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
                    var newMessage = $"{user}: {message}";
                    //DisplayMessage(newMessage);
                    chatBox.AppendText(newMessage);
                }));
            });

            connection.On<string, string, string>("ReceiveRoomInfo", (roomname, user, message) =>
            {
                this.Invoke((Action)(() =>
                {
                    if (message.Equals("join"))
                    {
                        Player newPlayer = new Player(user, gameboard.startingBalance());
                        gameboard.joinPlayer(newPlayer);
                        gameboard.setRoomname(roomname);

                        labelRoom.Visible = textBoxRoom.Visible = buttonJoin.Visible = false;
                        chatBox.Visible = actionBox.Visible = walletBox.Visible = positionBox.Visible = sellBox.Visible = true;
                        labelChat.Visible = labelLog.Visible = labelWallet.Visible = labelPositions.Visible = true;
                        buttonMove.Visible = buttonEnd.Visible = buttonSell.Visible = buttonBuy.Visible = true;

                        if (gameboard.getActivePlayer().Equals(user))
                        {
                            buttonMove.Enabled = true;
                        }
                    }
                    else
                    {
                        gameboard.removePlayer(user);
                    }
                }));
            });

            connection.On<string, int, int>("ReceiveMoveInfo", (user, roll_1, roll_2) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.makeMove(user, roll_1 + roll_2);

                    var newMessage = $"{user}: ruszył się o {roll_1+roll_2} pola \n";
                    actionBox.AppendText(newMessage);
                    
                    if(user.Equals(gameboard.getUsername()))
                    {
                        buttonMove.Enabled = false;
                        buttonBuy.Enabled = buttonEnd.Enabled = true;
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

            connection.On<int, int>("ReceiveAuctionInfo", (field, price) =>
            {
                this.Invoke((Action)(() =>
                {

                }));
            });

            connection.On<string, int, int>("ReceiveWinner", (user, field, price) =>
            {
                this.Invoke((Action)(() =>
                {

                }));
            });

            connection.On<string>("ReceiveEndTurn", (user) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.nextTurn(user);
                    if(gameboard.getActivePlayer().Equals(user))
                    {
                        buttonMove.Enabled = true;
                    }
                }));
            });

            connection.On<string>("ReceiveResign", (user) =>
            {
                this.Invoke((Action)(() =>
                {
                    gameboard.resign(user);
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
                        textBox1.Visible = textBoxRoom.Visible = buttonJoin.Visible = true;
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
            string state;
            int currentField;

            public Player()
            {
                username = ""; walletBalance = 0; state = ""; currentField = 0;
            }

            public Player(string username, int walletBalance)
            {
                this.username = username; this.walletBalance = walletBalance;
                state = "rest"; currentField = 0;
            }

            public void setPlayerstate(string state)
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

            public string CheckState()
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
            string special;
            List<Player> presentPlayers;

            public Field(int position, int price, int tax)
            {
                this.position = position; this.price = price; this.tax = tax;
                owner = "none"; special = "none"; presentPlayers = new List<Player>();
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

            public string CheckSpecial()
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
                    else
                    {
                        temp = new Field(i, 300, 30);
                        fields.Add(temp);
                    }
                }
            }
            public void joinPlayer(Player newPlayer)
            {
                players.Add(newPlayer);
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
                for (int i = 0; i < players.Count(); i++)
                {
                    if (players[i].getUsername().Equals(username))
                    {
                        if (players.Count() > i + 1) turnId = i + 1;
                        break;
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
                return players.Count();
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

            public int getBuyId()
            {
                return getPlayerByUsername(my_username).getPos();
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
            await connection.InvokeAsync("SendPurchaseInfo", gameboard.getRoomname(), gameboard.getUsername(), gameboard.getBuyId());
        }

        private async void buttonEnd_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("SendEndTurn", gameboard.getRoomname(), gameboard.getUsername());
        }
    }
}
