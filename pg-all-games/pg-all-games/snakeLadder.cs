using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace pg_all_games
{
    public partial class snakeLadder : Form
    {

        path[] player1 = new path[101];
        path[] player2 = new path[101];
        Microsoft.VisualBasic.PowerPacks.OvalShape[] OS = new Microsoft.VisualBasic.PowerPacks.OvalShape[9];
        Random r;
        int[] didfirstmove = new int[3];

        int[] S = new int[4];
        int[] SNL = new int[4];
        int[] L = new int[6];
        int[] LNL = new int[6];

        int whosemove = 1;  //1 blue 2 pink
        int blue = 0, pink = 0; //position on board controller

        public snakeLadder()
        {
            InitializeComponent();
            for (int i = 0; i <= 100; i++)
            {
                player1[i] = new path();
                player2[i] = new path();

            }
            for (int i = 0; i <= 8; i++)
            {
                OS[i] = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            }

       

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnROLL_Click(object sender, EventArgs e)
        {
            r = new Random();

            int Val = r.Next(1, 7);
            setdice(Val);
            textBox1.Text = Val.ToString();

            switch (whosemove)
            {
                case 1:
                    if((blue + Val)<=100)
                    {
                        if (didfirstmove[1] == 0)
                        {
                            if ((Val == 1) || (Val == 6))
                            {
                                blue = 0;
                                beadblue.Top = player1[blue].y;
                                beadblue.Left = player1[blue].x;
                                didfirstmove[1] = 1;
                                label10.SendToBack();
                            }
                        }
                        else
                        {
                            int temp = (blue + Val);
                            blue = temp;
                            int S = ifSnake(blue);
                            blue = S;
                            int L = ifLadder(blue);
                            blue = L;

                            beadblue.Top = player1[blue].y;
                            beadblue.Left = player1[blue].x;
                            sendbacklabels();
                        }
                    }

                    if (blue == 100)
                    {
                        MessageBox.Show("Player blue Wins ");
                        btnROLL.Enabled = false;
                    }
                    whosemove = 2;
                    textBox2.Text = "Pink";
                    break;
                case 2 :
                    if ((pink + Val) <= 100)
                    {
                        if (didfirstmove[2] == 0)
                        {
                            if ((Val == 1) || (Val == 6))
                            {
                                pink = 0;
                                beadpink.Top = player2[pink].y;
                                beadpink.Left = player2[pink].x;
                                didfirstmove[2] = 1;
                                label10.SendToBack();
                            }

                        }
                        else
                        {
                            int temp = (pink + Val);
                            pink = temp;
                            int S = ifSnake(pink);
                            pink = S;
                            int L = ifLadder(pink);
                            pink = L;

                            beadpink.Top = player2[pink].y;
                            beadpink.Left = player2[pink].x;
                            sendbacklabels();
                        }
                    }

                    if (pink == 100)
                    {
                        MessageBox.Show("Player Pink Wins ");
                        btnROLL.Enabled = false;
                    }
                    whosemove = 1;
                    textBox2.Text = "Blue";
                    break;

            }
           

        }

        private void sendbacklabels()
        {
            label1.SendToBack();
            label2.SendToBack();
            label3.SendToBack();
            label4.SendToBack();
            label5.SendToBack();
            label6.SendToBack();
            label7.SendToBack();
            label8.SendToBack();
            label9.SendToBack();
            label10.SendToBack();
        }

        private int ifLadder(int temp)
        {
            for (int i = 0; i < L.Length; i++)
            {
                if (temp == L[i])
                {
                    return LNL[i]; }
            }
            return temp; 
        }

        private int ifSnake(int temp)
        {
            for (int i = 0; i < S.Length;i++ ){
                if (temp == S[i])
                {
                    return SNL[i]; }                 
            }
            return temp; 
        }

        private void setdice(int Val)
        {

            switch (Val)
            {
                case 1:
                    ovalShape11.BackColor = Color.Black;
                    ovalShape12.BackColor = Color.Black;
                    ovalShape13.BackColor = Color.Black;
                    ovalShape21.BackColor = Color.Black;
                    ovalShape22.BackColor = Color.Red;
                    ovalShape23.BackColor = Color.Black;
                    ovalShape31.BackColor = Color.Black;
                    ovalShape32.BackColor = Color.Black;
                    ovalShape33.BackColor = Color.Black;
                    break;
                case 2:
                    ovalShape11.BackColor = Color.Red;
                    ovalShape12.BackColor = Color.Black;
                    ovalShape13.BackColor = Color.Black;
                    ovalShape21.BackColor = Color.Black;
                    ovalShape22.BackColor = Color.Black;
                    ovalShape23.BackColor = Color.Black;
                    ovalShape31.BackColor = Color.Black;
                    ovalShape32.BackColor = Color.Black;
                    ovalShape33.BackColor = Color.Red;
                    break;
                case 3:
                    ovalShape11.BackColor = Color.Red;
                    ovalShape12.BackColor = Color.Black;
                    ovalShape13.BackColor = Color.Black;
                    ovalShape21.BackColor = Color.Black;
                    ovalShape22.BackColor = Color.Red;
                    ovalShape23.BackColor = Color.Black;
                    ovalShape31.BackColor = Color.Black;
                    ovalShape32.BackColor = Color.Black;
                    ovalShape33.BackColor = Color.Red;
                    break;
                case 4:
                    ovalShape11.BackColor = Color.Red;
                    ovalShape12.BackColor = Color.Black;
                    ovalShape13.BackColor = Color.Red;
                    ovalShape21.BackColor = Color.Black;
                    ovalShape22.BackColor = Color.Black;
                    ovalShape23.BackColor = Color.Black;
                    ovalShape31.BackColor = Color.Red;
                    ovalShape32.BackColor = Color.Black;
                    ovalShape33.BackColor = Color.Red;
                    break;
                case 5:
                    ovalShape11.BackColor = Color.Red;
                    ovalShape12.BackColor = Color.Black;
                    ovalShape13.BackColor = Color.Red;
                    ovalShape21.BackColor = Color.Black;
                    ovalShape22.BackColor = Color.Red;
                    ovalShape23.BackColor = Color.Black;
                    ovalShape31.BackColor = Color.Red;
                    ovalShape32.BackColor = Color.Black;
                    ovalShape33.BackColor = Color.Red;
                    break;
                case 6:
                    ovalShape11.BackColor = Color.Red;
                    ovalShape12.BackColor = Color.Black;
                    ovalShape13.BackColor = Color.Red;
                    ovalShape21.BackColor = Color.Red;
                    ovalShape22.BackColor = Color.Black;
                    ovalShape23.BackColor = Color.Red;
                    ovalShape31.BackColor = Color.Red;
                    ovalShape32.BackColor = Color.Black;
                    ovalShape33.BackColor = Color.Red;
                    break;
                case 10:
                    ovalShape11.BackColor = Color.Black;
                    ovalShape12.BackColor = Color.Black;
                    ovalShape13.BackColor = Color.Black;
                    ovalShape21.BackColor = Color.Black;
                    ovalShape22.BackColor = Color.Black;
                    ovalShape23.BackColor = Color.Black;
                    ovalShape31.BackColor = Color.Black;
                    ovalShape32.BackColor = Color.Black;
                    ovalShape33.BackColor = Color.Black;
                    break;

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Form1_Load(null,null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

             whosemove = 1;  //1 blue 2 pink
             blue = 0; pink = 0; //position on board controller
             btnROLL.Enabled = true;

             beadblue.Top = 620;
             beadblue.Left =10;
             beadpink.Top = 620;
             beadpink.Left = 50;
            //setdice...set move
             setdice(10);
             textBox1.Text = "";
             textBox2.Text = "blue";


            didfirstmove[0] = 0;
            didfirstmove[1] = 0;  //red
            didfirstmove[2] = 0;  //pink

            {
                player1[0].x = 20;
                player1[1].x = 20; player1[2].x = 80; player1[3].x = 140; player1[4].x = 200; player1[5].x = 260; player1[6].x = 320; player1[7].x = 380; player1[8].x = 440; player1[9].x = 500; player1[10].x = 560;
                player1[11].x = 560; player1[12].x = 500; player1[13].x = 440; player1[14].x = 380; player1[15].x = 320; player1[16].x = 260; player1[17].x = 200; player1[18].x = 140; player1[19].x = 80; player1[20].x = 20;
                player1[21].x = 20; player1[22].x = 80; player1[23].x = 140; player1[24].x = 200; player1[25].x = 260; player1[26].x = 320; player1[27].x = 380; player1[28].x = 440; player1[29].x = 500; player1[30].x = 560;
                player1[31].x = 560; player1[32].x = 500; player1[33].x = 440; player1[34].x = 380; player1[35].x = 320; player1[36].x = 260; player1[37].x = 200; player1[38].x = 140; player1[39].x = 80; player1[40].x = 20;
                player1[41].x = 20; player1[42].x = 80; player1[43].x = 140; player1[44].x = 200; player1[45].x = 260; player1[46].x = 320; player1[47].x = 380; player1[48].x = 440; player1[49].x = 500; player1[50].x = 560;
                player1[51].x = 560; player1[52].x = 500; player1[53].x = 440; player1[54].x = 380; player1[55].x = 320; player1[56].x = 260; player1[57].x = 200; player1[58].x = 140; player1[59].x = 80; player1[60].x = 20;
                player1[61].x = 20; player1[62].x = 80; player1[63].x = 140; player1[64].x = 200; player1[65].x = 260; player1[66].x = 320; player1[67].x = 380; player1[68].x = 440; player1[69].x = 500; player1[70].x = 560;
                player1[71].x = 560; player1[72].x = 500; player1[73].x = 440; player1[74].x = 380; player1[75].x = 320; player1[76].x = 260; player1[77].x = 200; player1[78].x = 140; player1[79].x = 80; player1[80].x = 20;
                player1[81].x = 20; player1[82].x = 80; player1[83].x = 140; player1[84].x = 200; player1[85].x = 260; player1[86].x = 320; player1[87].x = 380; player1[88].x = 440; player1[89].x = 500; player1[90].x = 560;
                player1[91].x = 560; player1[92].x = 500; player1[93].x = 440; player1[94].x = 380; player1[95].x = 320; player1[96].x = 260; player1[97].x = 200; player1[98].x = 140; player1[99].x = 80; player1[100].x = 20;


                player1[0].y = 560;
                player1[1].y = 560; player1[2].y = 560; player1[3].y = 560; player1[4].y = 560; player1[5].y = 560; player1[6].y = 560; player1[7].y = 560; player1[8].y = 560; player1[9].y = 560; player1[10].y = 560;
                player1[11].y = 500; player1[12].y = 500; player1[13].y = 500; player1[14].y = 500; player1[15].y = 500; player1[16].y = 500; player1[17].y = 500; player1[18].y = 500; player1[19].y = 500; player1[20].y = 500;
                player1[21].y = 440; player1[22].y = 440; player1[23].y = 440; player1[24].y = 440; player1[25].y = 440; player1[26].y = 440; player1[27].y = 440; player1[28].y = 440; player1[29].y = 440; player1[30].y = 440;
                player1[31].y = 380; player1[32].y = 380; player1[33].y = 380; player1[34].y = 380; player1[35].y = 380; player1[36].y = 380; player1[37].y = 380; player1[38].y = 380; player1[39].y = 380; player1[40].y = 380;
                player1[41].y = 325; player1[42].y = 325; player1[43].y = 325; player1[44].y = 325; player1[45].y = 325; player1[46].y = 325; player1[47].y = 325; player1[48].y = 325; player1[49].y = 325; player1[50].y = 325;
                player1[51].y = 260; player1[52].y = 260; player1[53].y = 260; player1[54].y = 260; player1[55].y = 260; player1[56].y = 260; player1[57].y = 260; player1[58].y = 260; player1[59].y = 260; player1[60].y = 260;
                player1[61].y = 200; player1[62].y = 200; player1[63].y = 200; player1[64].y = 200; player1[65].y = 200; player1[66].y = 200; player1[67].y = 200; player1[68].y = 200; player1[69].y = 200; player1[70].y = 200;
                player1[71].y = 140; player1[72].y = 140; player1[73].y = 140; player1[74].y = 140; player1[75].y = 140; player1[76].y = 140; player1[77].y = 140; player1[78].y = 140; player1[79].y = 140; player1[80].y = 140;
                player1[81].y = 85; player1[82].y = 85; player1[83].y = 85; player1[84].y = 85; player1[85].y = 85; player1[86].y = 85; player1[87].y = 85; player1[88].y = 85; player1[89].y = 85; player1[90].y = 85;
                player1[91].y = 25; player1[92].y = 25; player1[93].y = 25; player1[94].y = 25; player1[95].y = 25; player1[96].y = 25; player1[97].y = 25; player1[98].y = 25; player1[99].y = 25; player1[100].y = 25;


                player2[0].x = 40;
                player2[1].x = 40; player2[2].x = 100; player2[3].x = 160; player2[4].x = 220; player2[5].x = 280; player2[6].x = 340; player2[7].x = 400; player2[8].x = 460; player2[9].x = 520; player2[10].x = 580;
                player2[11].x = 580; player2[12].x = 520; player2[13].x = 460; player2[14].x = 400; player2[15].x = 340; player2[16].x = 280; player2[17].x = 220; player2[18].x = 160; player2[19].x = 100; player2[20].x = 40;
                player2[21].x = 40; player2[22].x = 100; player2[23].x = 160; player2[24].x = 220; player2[25].x = 280; player2[26].x = 340; player2[27].x = 400; player2[28].x = 460; player2[29].x = 520; player2[30].x = 580;
                player2[31].x = 580; player2[32].x = 520; player2[33].x = 460; player2[34].x = 400; player2[35].x = 340; player2[36].x = 280; player2[37].x = 220; player2[38].x = 160; player2[39].x = 100; player2[40].x = 40;
                player2[41].x = 40; player2[42].x = 100; player2[43].x = 160; player2[44].x = 220; player2[45].x = 280; player2[46].x = 340; player2[47].x = 400; player2[48].x = 460; player2[49].x = 520; player2[50].x = 580;
                player2[51].x = 580; player2[52].x = 520; player2[53].x = 460; player2[54].x = 400; player2[55].x = 340; player2[56].x = 280; player2[57].x = 220; player2[58].x = 160; player2[59].x = 100; player2[60].x = 40;
                player2[61].x = 40; player2[62].x = 100; player2[63].x = 160; player2[64].x = 220; player2[65].x = 280; player2[66].x = 340; player2[67].x = 400; player2[68].x = 460; player2[69].x = 520; player2[70].x = 580;
                player2[71].x = 580; player2[72].x = 520; player2[73].x = 460; player2[74].x = 400; player2[75].x = 340; player2[76].x = 280; player2[77].x = 220; player2[78].x = 160; player2[79].x = 100; player2[80].x = 40;
                player2[81].x = 40; player2[82].x = 100; player2[83].x = 160; player2[84].x = 220; player2[85].x = 280; player2[86].x = 340; player2[87].x = 400; player2[88].x = 460; player2[89].x = 520; player2[90].x = 580;
                player2[91].x = 580; player2[92].x = 520; player2[93].x = 460; player2[94].x = 400; player2[95].x = 340; player2[96].x = 280; player2[97].x = 220; player2[98].x = 160; player2[99].x = 100; player2[100].x = 40;

                player2[0].y = 580;
                player2[1].y = 580; player2[2].y = 580; player2[3].y = 580; player2[4].y = 580; player2[5].y = 580; player2[6].y = 580; player2[7].y = 580; player2[8].y = 580; player2[9].y = 580; player2[10].y = 580;
                player2[11].y = 520; player2[12].y = 520; player2[13].y = 520; player2[14].y = 520; player2[15].y = 520; player2[16].y = 520; player2[17].y = 520; player2[18].y = 520; player2[19].y = 520; player2[20].y = 520;
                player2[21].y = 460; player2[22].y = 460; player2[23].y = 460; player2[24].y = 460; player2[25].y = 460; player2[26].y = 460; player2[27].y = 460; player2[28].y = 460; player2[29].y = 460; player2[30].y = 460;
                player2[31].y = 400; player2[32].y = 400; player2[33].y = 400; player2[34].y = 400; player2[35].y = 400; player2[36].y = 400; player2[37].y = 400; player2[38].y = 400; player2[39].y = 400; player2[40].y = 400;
                player2[41].y = 345; player2[42].y = 345; player2[43].y = 345; player2[44].y = 345; player2[45].y = 345; player2[46].y = 345; player2[47].y = 345; player2[48].y = 345; player2[49].y = 345; player2[50].y = 345;
                player2[51].y = 280; player2[52].y = 280; player2[53].y = 280; player2[54].y = 280; player2[55].y = 280; player2[56].y = 280; player2[57].y = 280; player2[58].y = 280; player2[59].y = 280; player2[60].y = 280;
                player2[61].y = 220; player2[62].y = 220; player2[63].y = 220; player2[64].y = 220; player2[65].y = 220; player2[66].y = 220; player2[67].y = 220; player2[68].y = 220; player2[69].y = 220; player2[70].y = 220;
                player2[71].y = 160; player2[72].y = 160; player2[73].y = 160; player2[74].y = 160; player2[75].y = 160; player2[76].y = 160; player2[77].y = 160; player2[78].y = 160; player2[79].y = 160; player2[80].y = 160;
                player2[81].y = 105; player2[82].y = 105; player2[83].y = 105; player2[84].y = 105; player2[85].y = 105; player2[86].y = 105; player2[87].y = 105; player2[88].y = 105; player2[89].y = 105; player2[90].y = 105;
                player2[91].y = 45; player2[92].y = 45; player2[93].y = 45; player2[94].y = 45; player2[95].y = 45; player2[96].y = 45; player2[97].y = 45; player2[98].y = 45; player2[99].y = 45; player2[100].y = 45;

                S[0] = 36; S[1] = 96; S[2] = 98; S[3] = 72;
                SNL[0] = 2; SNL[1] = 9; SNL[2] = 64; SNL[3] = 46;
                L[0] = 24; L[1] = 21; L[2] = 64; L[3] = 53; L[4] = 28; L[5] = 16;
                LNL[0] = 60; LNL[1] = 63; LNL[2] = 88; LNL[3] = 75; LNL[4] = 56; LNL[5] = 32;



            }  // values of locations for player1 and player2.
        }
    }
}

    class path
    {
        public int x;
        public int y;

        public path()
        {
            x = new int();
            x = 0;
            y = new int();
            y = 0;
        }
    }

    class bead
    {
        public int loc;
        public int status;

        public bead()
        {
            loc = -5;
            status = 0;
        }
    }


