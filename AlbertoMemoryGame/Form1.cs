using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlbertoMemoryGame
{
    public partial class Form1 : Form
    {

        int colRowSize = 4;
        int movements = 0;
        int cardsBackQuantity = 0;
        List<string> numberedCards;
        List<string> scrambledCards;
        ArrayList selectedCards;
        PictureBox tempCard1;
        PictureBox tempCard2;
        int cardActual;


        public Form1()
        {
            InitializeComponent();
            startGame();
        }

        public void startGame()
        {
            timer1.Enabled = false;
            timer1.Stop();
            scoreCountNum.Text = "0";
            cardsBackQuantity = 0;
            movements = 0;
            gamePanel.Controls.Clear();
            numberedCards = new List<string>();
            scrambledCards = new List<string>();
            selectedCards= new ArrayList();

            for (int i = 0; i <= 8; i++)
            {
                numberedCards.Add(i.ToString());
                numberedCards.Add(i.ToString());
            }

            var randomNumber = new Random();
            var result = numberedCards.OrderBy(item => randomNumber.Next());

            foreach (string cardValue in result)
            {
                scrambledCards.Add(cardValue);
            }

            var panelTable = new TableLayoutPanel();
            panelTable.RowCount = colRowSize;
            panelTable.ColumnCount = colRowSize;

            for (int i = 0; i < colRowSize; i++)
            {
                var porcentage = 150f / (float)colRowSize - 10;
                panelTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, porcentage));
                panelTable.RowStyles.Add(new RowStyle(SizeType.Percent, porcentage));
            }

            int recordsCounter = 1;

            for (var i = 0; i < colRowSize; i++)
            {
                for (var j = 0; j < colRowSize; j++)
                {
                    var cardsGame = new PictureBox();
                    cardsGame.Name = string.Format("{0}", recordsCounter);
                    cardsGame.Dock = DockStyle.Fill;
                    cardsGame.SizeMode = PictureBoxSizeMode.StretchImage;
                    cardsGame.Image = Properties.Resources.cardBack;
                    cardsGame.Cursor = Cursors.Hand;
                    cardsGame.Click += btnCard_Click;
                    panelTable.Controls.Add(cardsGame, j, i);
                    recordsCounter++;
                }
            }

            panelTable.Dock = DockStyle.Fill;
            gamePanel.Controls.Add(panelTable);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int TimeBackCard = 1;
            if (TimeBackCard == 1)
            {
                tempCard1.Image = Properties.Resources.cardBack;
                tempCard2.Image = Properties.Resources.cardBack;
                selectedCards.Clear();
                TimeBackCard = 0;
                timer1.Stop();
            }
        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClosed_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            if (selectedCards.Count < 2)
            {
                movements++;
                scoreCountNum.Text = Convert.ToString(movements);
                var selectedUserCards = (PictureBox)sender;

                cardActual = Convert.ToInt32(scrambledCards[Convert.ToInt32(selectedUserCards.Name) - 1]);
                selectedUserCards.Image = recoverImage(cardActual);
                selectedCards.Add(selectedUserCards);

                if (selectedCards.Count == 2)
                {
                    tempCard1 = (PictureBox)selectedCards[0];
                    tempCard2 = (PictureBox)selectedCards[1];
                    int card1 = Convert.ToInt32(scrambledCards[Convert.ToInt32(tempCard1.Name) - 1]);
                    int card2 = Convert.ToInt32(scrambledCards[Convert.ToInt32(tempCard2.Name) - 1]);

                    if (card1 != card2)
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                    else
                    {
                        cardsBackQuantity++;
                        if (cardsBackQuantity > 7)
                        {
                            MessageBox.Show("El Juego Terminó");
                        }
                        tempCard1.Enabled = false;
                        tempCard2.Enabled = false;
                        selectedCards.Clear();
                    }
                }
            }
        }

        public Bitmap recoverImage(int imageNumber) {
            Bitmap tmpImg = new Bitmap(200, 100);
            switch (imageNumber)
            {
                case 0: tmpImg = Properties.Resources.cardBack;
                    break;
                default: tmpImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + imageNumber);
                    break;
            }
            return tmpImg;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            startGame();
        }

    }
}
