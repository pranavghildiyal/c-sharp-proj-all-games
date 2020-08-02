using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pg_all_games
{
    public partial class chessLeague : Form
    {
        PathMethods pathMethods;
        Button[,] positionBlocks;

        int boardSize = 9;
        int move1X = 0, move1Y = 0;
        string soldierString = "S", elephantString = "E", horseString = "H", camelString = "C", queenString = "Q", kingString = "K";
        public int soldierBinitPos = 7, soldierWInitPos = 2;
        Position fromPosition, toPosition;

        Color playerBColor, playerWColor;

        public chessLeague()
        {
            InitializeComponent();
            initializeGame();
        }

        private void initializeGame()
        {
            pathMethods = new PathMethods();
            positionBlocks = new Button[boardSize + 1, boardSize + 1];
        }

        private void chessLeague_Load(object sender, EventArgs e)
        {
            loadForm();
        }

        private void loadForm()
        {
            for (int ivar = 1; ivar <= boardSize; ivar++)
            {
                for (int jvar = 1; jvar <= boardSize; jvar++)
                {
                    positionBlocks[ivar,jvar] = new Button();
                    positionBlocks[ivar,jvar] = (Button)Controls["button" + ivar + "" + jvar];
                }
            }

            playerBColor = Color.Black;
            playerWColor = Color.WhiteSmoke;

            soldierString = "S"; 
            elephantString = "E"; 
            horseString = "H"; 
            camelString = "C";
            queenString = "Q"; 
            kingString = "K";
        }

        /**
         * Method 1 - For common button Click Methods for the Grid of Buttons
         * This returns the Number value of button eg: 23 for button23
         **/
        public int getButtonNumber(object sender)
        {
            Button b = sender as Button;
            String s = b.Name;
            s = s.Replace("button", "");
            return Convert.ToInt32(s);
        }

        /**
         * Method 2 - For common button Click Methods for the Grid of Buttons
         * This is the click middleware- Click of button executes this,
         * which calls the generic Click method, providing Number value
         * of the button which was clicked by using Method 1.
         **/
        private void buttonAB_Click(object sender, EventArgs e)
        {
            button_Click(getButtonNumber(sender));
        }

        /**
         * Method 3 - For common button Click Methods for the Grid of Buttons
         * This is the actual code that shuld be executed when any of the 
         * button from the Grid of Button is clicked.
         **/
        private void button_Click(int p)
        {
            {
                performAction(p / 10, p % 10);
            }
        }

        private void performAction(int posX, int posY)
        {
            textBox1.Text = "performAction";
            if((move1X == 0) && (move1Y ==0))
            {
                move1X = posX;
                move1Y = posY;
            }
            else
            {
                Position fromPosition = new Position(move1X, move1Y);
                Position toPosition = new Position(posX, posY);
                switch (positionBlocks[move1X, move1Y].Text)
                {
                    case "S":
                        moveSoldier(fromPosition, toPosition);
                        break;
                    case "E":
                        moveElephant(fromPosition, toPosition);
                        break;
                    case "H": //2 1/2
                        moveHorse(fromPosition, toPosition);
                        break;
                    case "C":
                        moveCamel(fromPosition, toPosition);
                        break;
                    case "Q":
                        break;
                    case "K":
                        break;
                }

                resetMove1Vars();
            }
        }

        private void moveCamel(Position fromPosition, Position toPosition)
        {
            //preChecks
            textBox1.Text = "moveH val : " + fromPosition.xPos;
            Position[] path = pathMethods.getPathForCamel(fromPosition, toPosition, positionBlocks);

            for (int ivar = 1; ivar < path.Length - 1; ivar++)
            {
                if (positionBlocks[path[ivar].xPos, path[ivar].yPos].Text != "")
                {
                    return;
                }
            }

            Color foreColor = positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor;
            for (int ivar = 1; ivar < path.Length; ivar++)
            {
                textBox1.Text += ivar + ",";
                positionBlocks[path[ivar - 1].xPos, path[ivar - 1].yPos].Text = "";
                positionBlocks[path[ivar - 1].xPos, path[ivar - 1].yPos].ForeColor = Color.Blue;
                positionBlocks[path[ivar - 1].xPos, path[ivar - 1].yPos].FlatAppearance.BorderColor = Color.Blue;

                positionBlocks[path[ivar].xPos, path[ivar].yPos].Text = camelString;
                positionBlocks[path[ivar].xPos, path[ivar].yPos].ForeColor = foreColor;
                positionBlocks[path[ivar].xPos, path[ivar].yPos].FlatAppearance.BorderColor = foreColor;
                //afterKillMethod(); TODO
            }
        }

        private void moveHorse(Position fromPosition, Position toPosition)
        {
            //preChecks
            textBox1.Text = "moveH val : " + fromPosition.xPos;
            Position[] path = pathMethods.getPathForHorse(fromPosition, toPosition, positionBlocks);

            Color foreColor = positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor;
            if ((positionBlocks[toPosition.xPos, toPosition.yPos].ForeColor == foreColor))
            {
                return;
            }

            //BlankText, ForeColor, Border
            //text, ForeColor, Border

            positionBlocks[path[0].xPos, path[0].yPos].Text = "";
            positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor = Color.Blue;
            positionBlocks[fromPosition.xPos, fromPosition.yPos].FlatAppearance.BorderColor = Color.Blue;
            positionBlocks[path[3].xPos, path[3].yPos].Text = horseString;
            positionBlocks[path[3].xPos, path[3].yPos].ForeColor = foreColor;
            positionBlocks[path[3].xPos, path[3].yPos].FlatAppearance.BorderColor = foreColor;
            //afterKillMethod(); TODO
        }

        private void moveElephant(Position fromPosition, Position toPosition)
        {
            //preChecks
            textBox1.Text = "moveE val : " + fromPosition.xPos;
            Position[] path = pathMethods.getPathForElephant(fromPosition, toPosition, positionBlocks);

            for (int ivar = 1; ivar < path.Length - 1; ivar++)
            {
                if (positionBlocks[path[ivar].xPos, path[ivar].yPos].Text != "")
                {
                    return;
                }
            }

            //if (positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor == playerBColor)
            //{ }
            //else if (positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor == playerWColor)
            //{ }

            Color foreColor = positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor;
            for (int ivar = 1; ivar < path.Length; ivar++)
            {
                textBox1.Text += ivar + ",";
                positionBlocks[path[ivar - 1].xPos, path[ivar - 1].yPos].Text = "";
                positionBlocks[path[ivar].xPos, path[ivar].yPos].Text = elephantString;
                positionBlocks[path[ivar].xPos, path[ivar].yPos].ForeColor = foreColor;
                positionBlocks[path[ivar].xPos, path[ivar].yPos].FlatAppearance.BorderColor = foreColor;
                //afterKillMethod(); TODO
            }
        }

        private void moveSoldier(Position fromPosition, Position toPosition)
        {
            textBox1.Text = "moveS val : " + fromPosition.xPos;
            Position[] path = pathMethods.getPathForSoldier(fromPosition, toPosition, positionBlocks);

            if (positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor == playerBColor)
            {
                if (fromPosition.yPos != toPosition.yPos) //Kill (enemy)  moves
                {
                    if ((((fromPosition.yPos - toPosition.yPos) == 1) || ((fromPosition.yPos - toPosition.yPos) == -1)) && (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") && ((fromPosition.xPos - toPosition.xPos) == 1) && (positionBlocks[toPosition.xPos,toPosition.yPos].ForeColor != playerBColor))
                    {
                        textBox1.Text = "CanMove to X,Y " + toPosition.xPos + ", " + toPosition.yPos;
                        positionBlocks[fromPosition.xPos, fromPosition.yPos].Text = "";
                        positionBlocks[toPosition.xPos, toPosition.yPos].Text = soldierString;
                        //TODO add to eliminated;
                        positionBlocks[toPosition.xPos, toPosition.yPos].ForeColor = playerBColor;
                        positionBlocks[toPosition.xPos, toPosition.yPos].FlatAppearance.BorderColor = playerBColor;
                        return;
                    }
                    else
                    {
                        textBox1.Text = "return";
                        return;
                    }
                }

                if (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") //disallow non-kill moves if position != empty
                {
                    return;
                }

                if (fromPosition.xPos == 7) //moves - initial move can be 1 or 2 steps
                {
                    if (((fromPosition.xPos - toPosition.xPos) == 1) || ((fromPosition.xPos - toPosition.xPos) == 2))
                    {
                        for (int ivar = fromPosition.xPos; ivar > toPosition.xPos; ivar--)
                        {
                            textBox1.Text += ivar + ",";
                            positionBlocks[ivar, fromPosition.yPos].Text = "";
                            positionBlocks[ivar - 1, fromPosition.yPos].Text = soldierString;
                            positionBlocks[ivar - 1, fromPosition.yPos].ForeColor = playerBColor;
                            positionBlocks[ivar - 1, fromPosition.yPos].FlatAppearance.BorderColor = playerBColor;
                        }
                    }
                }
                else //moves - non -initial move, canMove only if trying to move 1 block in same Colmn
                {
                    if (((fromPosition.xPos - toPosition.xPos) == 1))
                    {
                        positionBlocks[fromPosition.xPos, fromPosition.yPos].Text = "";
                        positionBlocks[fromPosition.xPos - 1, fromPosition.yPos].Text = soldierString;
                        positionBlocks[fromPosition.xPos - 1, fromPosition.yPos].ForeColor = playerBColor;
                        positionBlocks[fromPosition.xPos - 1, fromPosition.yPos].FlatAppearance.BorderColor = playerBColor;
                    }
                }
            }
            else if (positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor == playerWColor)
            {
                textBox1.Text = "White";
                if (fromPosition.yPos != toPosition.yPos) //Kill (enemy)  moves
                {
                    if ((((fromPosition.yPos - toPosition.yPos) == 1) || ((fromPosition.yPos - toPosition.yPos) == -1)) && (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") && ((fromPosition.xPos - toPosition.xPos) == -1) && (positionBlocks[toPosition.xPos, toPosition.yPos].ForeColor != playerWColor))
                    {
                        textBox1.Text = "CanMove to X,Y " + toPosition.xPos + ", " + toPosition.yPos;
                        positionBlocks[fromPosition.xPos, fromPosition.yPos].Text = "";
                        positionBlocks[toPosition.xPos, toPosition.yPos].Text = soldierString;
                        //TODO add to eliminated;
                        positionBlocks[toPosition.xPos, toPosition.yPos].ForeColor = playerWColor;
                        positionBlocks[toPosition.xPos, toPosition.yPos].FlatAppearance.BorderColor = playerWColor;
                        return;
                    }
                    else
                    {
                        textBox1.Text = "return";
                        return;
                    }
                }

                if (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") //disallow non-kill moves if position != empty
                {
                    return;
                }

                if (fromPosition.xPos == 2) //moves - initial move can be 1 or 2 steps
                {
                    textBox1.Text += (fromPosition.xPos - toPosition.xPos).ToString();
                    if (((fromPosition.xPos - toPosition.xPos) == -1) || ((fromPosition.xPos - toPosition.xPos) == -2))
                    {
                        for (int ivar = fromPosition.xPos; ivar < toPosition.xPos; ivar++)
                        {
                            textBox1.Text += ivar + ",";
                            positionBlocks[ivar, fromPosition.yPos].Text = "";
                            positionBlocks[ivar + 1, fromPosition.yPos].Text = soldierString;
                            positionBlocks[ivar + 1, fromPosition.yPos].ForeColor = playerWColor;
                            positionBlocks[ivar + 1, fromPosition.yPos].FlatAppearance.BorderColor = playerWColor;
                        }
                    }
                }
                else //moves - non -initial move, canMove only if trying to move 1 block in same Colmn
                {
                    if (((fromPosition.xPos - toPosition.xPos) == -1))
                    {
                        positionBlocks[fromPosition.xPos, fromPosition.yPos].Text = "";
                        positionBlocks[fromPosition.xPos + 1, fromPosition.yPos].Text = soldierString;
                        positionBlocks[fromPosition.xPos + 1, fromPosition.yPos].ForeColor = playerWColor;
                        positionBlocks[fromPosition.xPos + 1, fromPosition.yPos].FlatAppearance.BorderColor = playerWColor;
                    }
                }
            }
        }

        private void resetMove1Vars()
        {
            move1X = 0;
            move1Y = 0;
        }
            
    }

    class Position
    {
        public int xPos;
        public int yPos;

        public Position()
        {
            xPos = 0;
            yPos = 0;
        }

        public Position(int xVal, int yVal)
        {
            xPos = xVal;
            yPos = yVal;
        }

        public Position getPosition(Button obj)
        {
            return new Position(obj.Left, obj.Top);
        }

        public Position getPosition(int xVal, int yVal)
        {
            return new Position(xVal, yVal);
        }
    }

    class PathMethods
    {
        static Color playerBColor;
        static Color playerWColor;

        static PathMethods()
        {
            playerBColor = Color.Black;
            playerWColor = Color.WhiteSmoke;
        }

        public Position[] getPathForSoldier(Position fromPosition, Position toPosition, Button[,] positionBlocks)
        {
            Position[] path = null;

            if (positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor == PathMethods.playerBColor)
            {
                if (fromPosition.yPos != toPosition.yPos) //Kill (enemy)  moves
                {
                    if ((((fromPosition.yPos - toPosition.yPos) == 1) || ((fromPosition.yPos - toPosition.yPos) == -1)) && (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") && ((fromPosition.xPos - toPosition.xPos) == 1) && (positionBlocks[toPosition.xPos,toPosition.yPos].ForeColor != playerBColor))
                    {
                        path = new Position[2]{fromPosition, toPosition};
                    }
                    else
                    {
                        return path;//invalid move So, returning emptyPath
                    }
                }

                //if (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") //disallow non-kill moves if position != empty
                //{
                //    return path;
                //}

                if (fromPosition.xPos == 7) //moves - initial move can be 1 or 2 steps
                {
                    int posDiff = fromPosition.xPos - toPosition.xPos;
                    if (((posDiff) == 1) || ((posDiff) == 2))
                    {
                        path = new Position[posDiff + 1];
                        for (int ivar = fromPosition.xPos, jvar = 0; ivar >= toPosition.xPos; ivar--,jvar++)
                        {
                            path[jvar] = new Position(ivar, fromPosition.yPos);
                        }
                    }
                }
                else //moves - non -initial move, canMove only if trying to move 1 block in same Colmn
                {
                    if (((fromPosition.xPos - toPosition.xPos) == 1))
                    {
                        path = new Position[2] { fromPosition, toPosition };
                    }
                }
            }
            else if (positionBlocks[fromPosition.xPos, fromPosition.yPos].ForeColor == playerWColor)
            {
                if (fromPosition.yPos != toPosition.yPos) //Kill (enemy)  moves
                {
                    if ((((fromPosition.yPos - toPosition.yPos) == 1) || ((fromPosition.yPos - toPosition.yPos) == -1)) && (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") && ((fromPosition.xPos - toPosition.xPos) == -1) && (positionBlocks[toPosition.xPos, toPosition.yPos].ForeColor != playerWColor))
                    {
                        path = new Position[2] { fromPosition, toPosition };
                        return path;
                    }
                    else
                    {
                        return path;
                    }
                }

                if (positionBlocks[toPosition.xPos, toPosition.yPos].Text != "") //disallow non-kill moves if position != empty
                {
                    return path;
                }

                if (fromPosition.xPos == 2) //moves - initial move can be 1 or 2 steps
                {
                    int posDiff = fromPosition.xPos - toPosition.xPos;
                    if (((posDiff) == -1) || ((posDiff) == -2))
                    {
                        path = new Position[posDiff + 1];
                        for (int ivar = fromPosition.xPos, jvar = 0; ivar <= toPosition.xPos; ivar++, jvar++)
                        {
                            path[ivar] = new Position(ivar, fromPosition.yPos);
                        }
                    }
                }
                else //moves - non -initial move, canMove only if trying to move 1 block in same Colmn
                {
                    if (((fromPosition.xPos - toPosition.xPos) == -1))
                    {
                        path = new Position[2] { fromPosition, toPosition };
                    }
                }
            }

            return path;
        }

        public Position[] getPathForElephant(Position fromPosition, Position toPosition, Button[,] positionBlocks)
        {
            Position[] path = null;
            if ((fromPosition.xPos == toPosition.xPos) && (fromPosition.yPos != toPosition.yPos)) //H move
            {
                if (fromPosition.yPos > toPosition.yPos) //Leftward
                {
                    path = new Position[fromPosition.yPos - toPosition.yPos + 1];
                    path[0] = new Position(fromPosition.xPos, fromPosition.yPos);
                    for (int ivar = fromPosition.yPos + 1, jvar = 1; ivar >= toPosition.yPos; ivar--)
                    {
                        path[jvar] = new Position(fromPosition.xPos,ivar);
                    }
                }
                else if ((fromPosition.yPos < toPosition.yPos)) //Rightward
                {
                    path = new Position[toPosition.yPos - fromPosition.yPos + 1];
                    path[0] = new Position(fromPosition.xPos, fromPosition.yPos);
                    for (int ivar = fromPosition.yPos + 1, jvar = 1; ivar <= toPosition.yPos; ivar++)
                    {
                        path[jvar] = new Position(fromPosition.xPos, ivar);
                    }
                }
            }
            else if ((fromPosition.yPos == toPosition.yPos) && (fromPosition.xPos != toPosition.xPos))
            {
                if (fromPosition.xPos > toPosition.xPos) //Downward
                {
                    path = new Position[fromPosition.xPos - toPosition.xPos + 1];
                    path[0] = new Position(fromPosition.xPos, fromPosition.yPos);
                    for (int ivar = fromPosition.xPos + 1, jvar = 1; ivar >= toPosition.xPos; ivar--)
                    {
                        path[jvar] = new Position(fromPosition.yPos, ivar);
                    }
                }
                else if ((fromPosition.xPos < toPosition.xPos)) //Upward
                {
                    path = new Position[toPosition.xPos - fromPosition.xPos + 1];
                    path[0] = new Position(fromPosition.xPos, fromPosition.yPos);
                    for (int ivar = fromPosition.xPos + 1, jvar = 1; ivar <= toPosition.xPos; ivar++)
                    {
                        path[jvar] = new Position(fromPosition.yPos, ivar);
                    }
                }
            }
            return path;
        }

        public Position[] getPathForHorse(Position fromPosition, Position toPosition, Button[,] positionBlocks)
        {
            Position[] path = new Position[4];
            if(((fromPosition.xPos - toPosition.xPos) == 2) || ((fromPosition.xPos - toPosition.xPos) == -2) )
            {
                 if(((fromPosition.yPos - toPosition.yPos) == 1) || ((fromPosition.yPos - toPosition.yPos) == -1))
                 {
                     if (((fromPosition.xPos - toPosition.xPos) == 2))
                     {
                         for (int ivar = fromPosition.xPos, jvar = 0; ivar >= toPosition.xPos; ivar--, jvar++)
                         {
                             path[jvar] = new Position(ivar, fromPosition.yPos);
                         }
                         path[3] = toPosition;
                     }
                     else if (((fromPosition.xPos - toPosition.xPos) == -2))
                     {
                         for (int ivar = fromPosition.xPos, jvar = 0; ivar <= toPosition.xPos; ivar++, jvar++)
                         {
                             path[jvar] = new Position(ivar, fromPosition.yPos);
                         }
                         path[3] = toPosition;
                     }
                 }
            }
            else if (((fromPosition.xPos - toPosition.xPos) == 1) || ((fromPosition.xPos - toPosition.xPos) == -1))
            {
                if (((fromPosition.yPos - toPosition.yPos) == 2) || ((fromPosition.yPos - toPosition.yPos) == -2))
                {
                    if (((fromPosition.yPos - toPosition.yPos) == 2))
                    {
                        for (int ivar = fromPosition.yPos, jvar = 0; ivar >= toPosition.yPos; ivar--, jvar++)
                        {
                            path[jvar] = new Position(fromPosition.xPos, ivar);
                        }
                        path[3] = toPosition;
                    }
                    else if (((fromPosition.yPos - toPosition.yPos) == -2))
                    {
                        for (int ivar = fromPosition.yPos, jvar = 0; ivar <= toPosition.yPos; ivar++, jvar++)
                        {
                            path[jvar] = new Position(fromPosition.xPos, ivar);
                        }
                        path[3] = toPosition;
                    }
                }
            }
            return path;
        }

        public Position[] getPathForCamel(Position fromPosition, Position toPosition, Button[,] positionBlocks)
        {
            Position[] path = null;
            int xDiff = fromPosition.xPos - toPosition.xPos;
            int yDiff = toPosition.xPos - toPosition.yPos;

            if (xDiff < 0)
            {
                xDiff = -1 * xDiff;
            }

            if (yDiff < 0)
            {
                yDiff = -1 * yDiff;
            }

            if (!(xDiff == yDiff))
            {
                return path;
            }

            if (fromPosition.xPos > toPosition.xPos) //Up
            {
                if (fromPosition.yPos > toPosition.yPos) //right
                {
                    path = new Position[xDiff];
                    for (int ivar = fromPosition.xPos, jvar = 0, kvar = fromPosition.yPos; ivar >= toPosition.xPos; ivar--, jvar++, kvar--)
                    {
                        path[jvar] = new Position(ivar, kvar);
                    }
                }
                else if (fromPosition.yPos < toPosition.yPos) //left
                {
                    path = new Position[xDiff];
                    for (int ivar = fromPosition.xPos, jvar = 0, kvar = fromPosition.yPos; ivar >= toPosition.xPos; ivar--, jvar++, kvar++)
                    {
                        path[jvar] = new Position(ivar, kvar);
                    }
                }
            }
            else if (fromPosition.xPos < toPosition.xPos) //down
            {
                if (fromPosition.yPos > toPosition.yPos) //right
                {
                    path = new Position[xDiff];
                    for (int ivar = fromPosition.xPos, jvar = 0, kvar = fromPosition.yPos; ivar >= toPosition.xPos; ivar++, jvar++, kvar--)
                    {
                        path[jvar] = new Position(ivar, kvar);
                    }
                }
                else if (fromPosition.yPos < toPosition.yPos) //left
                {
                    path = new Position[xDiff];
                    for (int ivar = fromPosition.xPos, jvar = 0, kvar = fromPosition.yPos; ivar >= toPosition.xPos; ivar++, jvar++, kvar++)
                    {
                        path[jvar] = new Position(ivar, kvar);
                    }
                }
            }

            return path;
        }
        public Position[] getPathForQueen(Position fromPosition, Position toPosition)
        {
            return null;
        }
        public Position[] getPathForKing(Position fromPosition, Position toPosition)
        {
            return null;
        }

        public bool isPathValidForSoldier()
        {
            return false;
        }

        public bool isPathValidForElephant()
        {
            return false;
        }
        public bool isPathValidForHorse()
        {
            return false;
        }
        public bool isPathValidForCamel()
        {
            return false;
        }
        public bool isPathValidForQueen()
        {
            return false;
        }
        public bool isPathValidForKing()
        {
            return false;
        }
    }
}
