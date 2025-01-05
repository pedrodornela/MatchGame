using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;
        string historyTimer;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timerTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                historyTextBlock.Text = timerTextBlock.Text; 
                timerTextBlock.Text = $"{timerTextBlock.Text} - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = [

                "🐶","🐶",
                "🐺","🐺",
                "🐷","🐷",
                "🐮","🐮",
                "🐔","🐔",
                "🐸","🐸",
                "🐋","🐋",
                "🐴","🐴",
            ];

            Random random = new();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {

                if(textBlock.Name == "")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }                
            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;

        }

        TextBlock lastTextBlockClicked;
        bool isFirstClicked = true;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /* If the textBlock clicked is the first -> Then hidden it's self,
              save this content in lastTextBlockClicked
              and chance the variable isFirstClicked for false;
            */
            /* Else If the lastTextBlockClicked.Text = textBlock.Text -> it means ther pair was found,
                then the current textBlock is hidden and the isFirstClicked torns true, so that next 
                textBlock can be the first of the new pair;
             */
            /* Else the textBlock clicked isn't igual to lastTextBlockClicked and isn't the first clicked, it means the pair isn't igual.
               Then the lastTextBlockClicked return to be visible and isFirstClicked
               turns true again to restar the match
             */

            TextBlock textBlock = (TextBlock)sender;
            
            if (isFirstClicked == true)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                isFirstClicked = false;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                isFirstClicked = true;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                isFirstClicked = true;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)
            {
                SetUpGame();
            }               
    
        }

        private void historyTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}