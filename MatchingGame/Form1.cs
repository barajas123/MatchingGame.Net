using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // firstClicked points to the first Label control
        // that the player clicks, but it will be null
        // if the player hasnt clicked a label ye
        Label firstClicked = null;

        // secondClicked points to the second Label control
        // thats the player clicks
        Label secondClicked = null;


        // Use this Random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        /// <summary>
        /// Assign each ucon from the list of icons to a random square
        /// </summary>
        private void AssignIconsToSquares()
        {
            // The TableLayoutPanel has 16 labels,
            // and the icon list has 16 icons,
            // so an icon is pulled at random from the list,
            // and added to each label
        foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);


                }
            }
        
        }
        public Form1()
        {
            InitializeComponent();

            // Call AssignIconsToSquares() to assign an icon to each of the sqaures
            AssignIconsToSquares();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Every label's Click event is handled by this event hanlder
        /// </summary>
        /// <param name="sender">The label was clicked</param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if(clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                // if the firstClicked is null, this is the first icon
                // in the pair that the player clicked,
                // so set firstClicked to the label that the player
                // clicked , change its color to black , and return
                if(firstClicked == null) 
                {

                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                
                }

                // if the player gets this far , the timer isnt
                // running and the firstClicked inst null,
                //so this must be the second click
                // Set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // if the player gets this far, the timer isnt 
                // running and firstClicked isnt null
                // so this must be the second icon click
                // Set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if player won
                CheckForWinner();


                // If the player clicked two matching icons, keep them
                // black and reset firstClicked and secondClicked
                // so the player can click another icon
                if(firstClicked.Text == secondClicked.Text) 
                {
                    firstClicked = null; secondClicked = null;
                    return;
                    
                
                }


                // If the player gets this far, the player
                // clicked two different icons, so start
                // the timer (which will wait 3/4 of a second
                // and then hide the icons)
                timer1.Start();
            
            }

        }

        /// <summary>
        /// This timer is started when the player clicks
        /// two icons that dont match,
        /// so it counts three quarters of a second 
        /// and then turns itself off and hides both icons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked
            // so the next time a label is
            // clicked, the program knows its the first click
            firstClicked = null;
            secondClicked = null;
        }

        ///<summary>
        ///Check every icon to see if its matched, by comparing its
        ///foreground color to its background color
        /// If all of the icons are matched, the player wins
        /// </summary>
        private void CheckForWinner()
        {
        
            // Go through all of the labels in the TableLayoutPanel,
            // checking each one tto see if its icon is matched 
            foreach(Control control in tableLayoutPanel1.Controls) 
            {

                Label iconLabel = control as Label;

                if(iconLabel != null)
                {

                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;

                }
            }

            // If the loop didnt return , it didnt find
            // any unmatched icons
            // That means the user won
            // Show a message and close the form
            MessageBox.Show("You matched all of the icons!!","Congratulations");
            Close();
        
        
        }
    }
}
