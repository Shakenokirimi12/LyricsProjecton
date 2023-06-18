using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LyricsProjection
{
    /// <summary>
    /// LyricWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LyricWindow : Window
    {
        public LyricWindow()
        {
            InitializeComponent();
            LyricBoxA_F.Text = "";
            LyricBoxA_D.Text = "";
            LyricBoxA_U.Text = "";
            WindowStyle = WindowStyle.None; // ウィンドウのスタイルを非表示に設定
            WindowState = WindowState.Maximized; // ウィンドウを最大化して全画面表示
            Topmost = true; // ウィンドウを最前面に表示
        }

        public void SetLyric(string lyric, string destination)
        {
            if (lyric == "Clear")
            {
                LyricBoxA_F.Text = "";
                LyricBoxA_U.Text = "";
                LyricBoxA_D.Text = "";
                return;
            }
            if (destination == "F")
            {
                LyricBoxA_F.Text = lyric;
            }
            else if (destination == "SU")
            {
                LyricBoxA_U.Text += lyric;
            }
            else if (destination == "SD")
            {
                LyricBoxA_D.Text += lyric;

            }

        }

    }
}
