using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using BNetHelper;

namespace Version_Viewer
{
    public partial class MainForm : Form
    {
        private GameList games;
        private BNetHelper.BNetHelper bnet;

        public MainForm()
        {
            InitializeComponent();

            games = new GameList();
            gameDataListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            bnet = new BNetHelper.BNetHelper();
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
        
            Task.Run(() =>
            {
                Invoke(new Action(() =>
                {
                    gameSelectionBox.Enabled = false;
                    gameDataListView.Items.Clear();
                }));

                try
                {
                    var items = bnet.GetData(game.code, bgDLCheck.Checked ? DownloadMode.bgdl : DownloadMode.versions);

                    Invoke(new Action(() =>
                    {
                        gameDataListView.Columns.Clear();
                    }));

                    foreach(var header in items.headers)
                    {
                        Invoke(new Action(() =>
                        {
                            int size = 100;

                            string colName = header.Replace(" ", "");
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

                            if (!colName.Equals("productconfig", StringComparison.CurrentCultureIgnoreCase))
                                gameDataListView.Columns.Add(header, size, HorizontalAlignment.Left);
                        }));
                    }


                    foreach(var item in items.games)
                    {
                        ListViewItem lvItem = null;
                        var values = item.Values;

                        int idx = 0;
                        foreach(var val in values)
                        {
                            if(idx == 0)
                            {
                                lvItem = new ListViewItem(val);
                            }else
                            {
                                lvItem.SubItems.Add(val);
                            }

                            idx++;
                        }

                        if (lvItem != null)
                        {
                            Invoke(new Action(() =>
                            {
                                gameDataListView.Items.Add(lvItem);
                            }));
                        }
                    }
                }
                catch(Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("Game doesn't have any version info", "No Version Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }).ContinueWith((a) =>
            {
                Invoke(new Action(() =>
                {
                    gameSelectionBox.Enabled = true;
                }));
            });
        }
    }
}
