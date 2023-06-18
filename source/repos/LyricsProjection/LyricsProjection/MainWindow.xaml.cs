using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LyricsProjection
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string[] lyrics;  // 配列に歌詞を追加します



        private void RightButton_Click(object sender, RoutedEventArgs e)
        {

            // 次の歌詞をLyricWindowに反映させます
            if (currentIndex < lyrics.Length)
            {
                if (lyrics[currentIndex].Substring(0, 3) == "SU ")
                {
                    lyricWindow.SetLyric(lyrics[currentIndex].Substring(3, lyrics[currentIndex].Length - 3), "SU");
                    try
                    {
                        NextTextBlock.Text = lyrics[currentIndex + 1];
                    }
                    catch
                    {
                        NextTextBlock.Text = "[End of the song]";
                    }
                    NowTextBlock.Text = lyrics[currentIndex];

                }
                else if (lyrics[currentIndex].Substring(0, 3) == "SD ")
                {
                    lyricWindow.SetLyric(lyrics[currentIndex].Substring(3, lyrics[currentIndex].Length - 3), "SD");
                    try
                    {
                        NextTextBlock.Text = lyrics[currentIndex + 1];
                    }
                    catch
                    {
                        NextTextBlock.Text = "[End of the song]";

                    }
                    NowTextBlock.Text = lyrics[currentIndex];

                }
                else if (lyrics[currentIndex].Substring(0, 2) == "F ")
                {
                    lyricWindow.SetLyric(lyrics[currentIndex].Substring(2, lyrics[currentIndex].Length - 2), "F");
                    try
                    {
                        NextTextBlock.Text = lyrics[currentIndex + 1];
                    }
                    catch
                    {
                        NextTextBlock.Text = "[End of the song]";

                    }
                    NowTextBlock.Text = lyrics[currentIndex];

                }
                else if (lyrics[currentIndex] == "Clear")
                {
                    lyricWindow.SetLyric(lyrics[currentIndex], "F");
                    try
                    {
                        NextTextBlock.Text = lyrics[currentIndex + 1];
                    }
                    catch
                    {
                        NextTextBlock.Text = "[End of the song]";

                    }
                    NowTextBlock.Text = lyrics[currentIndex];

                }
                currentIndex++;
                LyricsListBox.Items.MoveCurrentToPosition(currentIndex);
            }
            else
            {
                lyricWindow.SetLyric("Clear", "F");
                NowTextBlock.Text = "[End of the song]";
                NextTextBlock.Text = "";
                RightButton.IsEnabled = false;
                return;
            }

        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {

        }



        private LyricWindow lyricWindow;
        private int currentIndex = 0;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            RightButton.IsEnabled = true;
            if (lyricWindow == null)
            {
                lyricWindow = new LyricWindow();
                lyricWindow.Closed += (s, args) => lyricWindow = null;
            }
            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.FirstOrDefault(s => !s.Primary);
            if (secondaryScreen != null)
            {
                lyricWindow.Left = secondaryScreen.Bounds.Left;
                lyricWindow.Top = secondaryScreen.Bounds.Top;
                lyricWindow.Width = secondaryScreen.Bounds.Width;
                lyricWindow.Height = secondaryScreen.Bounds.Height;
                lyricWindow.WindowState = WindowState.Normal;
            }
            lyricWindow.Show();
            StartButton.IsEnabled = false;
        }

        private void LoadLyrics_Click(object sender, RoutedEventArgs e)
        {
            // ファイル選択ダイアログを表示し、テキストファイルを選択させる
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "テキストファイル|*.txt";
            DialogResult result = openFileDialog.ShowDialog();

            // ファイルが選択された場合のみ処理を続行
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // ファイルパスからテキストファイルの内容を読み込む
                string filePath = openFileDialog.FileName;
                lyrics = File.ReadAllLines(filePath);
                currentIndex = 0;
                LyricsListBox.ItemsSource = lyrics;
                NextTextBlock.Text = lyrics[0];
                StartButton.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                lyricWindow.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                if (RightButton.IsEnabled == true)
                {
                    RightButton_Click(null, null);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (RightButton.IsEnabled == true && currentIndex >= 2)
                {
                    currentIndex -= 2;
                    RightButton_Click(null, null);
                }
            }
        }
    }
}
