using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Version_Viewer
{
    public partial class MainForm : Form
    {
        GameList games;
        public MainForm()
        {
            InitializeComponent();

            games = new GameList();
            gameDataListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            games.Games = JsonConvert.DeserializeObject<List<GameList.RootObject>>(System.IO.File.ReadAllText("game_codes.json"));

            foreach(var game in games.Games)
            {
                gameSelectionBox.Items.Add(game.name);
            }
        }

        private void gameSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameList.RootObject game = games.Games[gameSelectionBox.SelectedIndex];
            var url = $"http://us.patch.battle.net:1119/{game.code}/{(bgDLCheck.Checked ? "bgdl" : "versions")}?nocache={DateTime.Now.Millisecond}";
        
            Task.Run(() =>
            {
                Invoke(new Action(() =>
                {
                    gameSelectionBox.Enabled = false;
                    gameDataListView.Items.Clear();
                }));


                using(var wc = new WebClient())
                {
                    try {
                        var data = wc.DownloadString(url);

                        var lines = Lines(data.Trim());


                        Invoke(new Action(() =>
                        {
                            gameDataListView.Columns.Clear();
                        }));

                        for (var i = 0; i < lines.Length; i++)
                        {
                            var line = lines[i].Split('|');

                            if (line.Length <= 1) continue;

                            ListViewItem lvItem = null;
                            if (i > 0)
                            {
                                lvItem = new ListViewItem(line[0]);
                            }

                            for (var ix = 0; ix < line.Length; ix++)
                            {
                                if (i == 0 && gameDataListView.Columns.Count < line.Length)
                                {
                                    Invoke(new Action(() =>
                                    {
                                        int size = 100;

                                        string colName = line[ix].Split('!')[0];
                                        if (colName.Equals("region", StringComparison.CurrentCultureIgnoreCase) || colName.Equals("buildid", StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            size = 50;
                                        }

                                        if (colName.Equals("versionsname", StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            size = 85;
                                        }

                                        if (colName.Equals("buildconfig", StringComparison.CurrentCultureIgnoreCase) || colName.Equals("cdnconfig", StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            size = 200;
                                        }

                                        if(!colName.Equals("productconfig", StringComparison.CurrentCultureIgnoreCase))
                                            gameDataListView.Columns.Add(SplitCamelCase(colName), size, HorizontalAlignment.Left);
                                    }));
                                }
                                else
                                {
                                    if (lvItem != null)
                                        if(ix > 0)
                                            if(line.Length - 1 > ix)
                                                lvItem.SubItems.Add(line[ix]);
                                }
                            }

                            if (lvItem != null)
                            {
                                Invoke(new Action(() =>
                                {
                                    gameDataListView.Items.Add(lvItem);
                                }));
                            }
                        }
                    } catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show( "Game doesn't have any version info", "No Version Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }));
                    }
                }
            }).ContinueWith((a) =>
            {
                Invoke(new Action(() =>
                {
                    gameSelectionBox.Enabled = true;
                }));
            });
        }

        public string[] Lines(string source)
        {
            return source.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        public string SplitCamelCase(string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
    }
}
