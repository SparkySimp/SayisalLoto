using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NTP_20221222_PRNG_4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        int[][] __sayilar = new int[6][];
        Random __prng = new Random();
        private void btnDoldur_Click(object sender, EventArgs e)
        {
            ListBox[] listBoxes = (from Control ctl in Controls where ctl is ListBox _ select (ListBox)ctl).ToArray();
            foreach (var item in listBoxes)
            {
                item.SelectedIndices.Clear();
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int sayi = __prng.Next(1, 20);
                    do
                    {
                        sayi = __prng.Next(1, 20);
                    } while (Array.IndexOf(__sayilar[i], sayi) != -1);
                    __sayilar[i][j] = sayi;
                }
            }

            for (int i = 1; i <= 6; i++)
            {
                ListBox currlb = ((ListBox)Controls[$"listBox{i}"]);
                currlb.Items.Clear();
                foreach (var j in __sayilar[i - 1])
                {
                    currlb.Items.Add(j);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                __sayilar[i] = new int[6];
            }

            Label[] labels = (from Control ctl in Controls where ctl is Label _ && ctl.Name != "lblToplar" select (Label)ctl).ToArray();
            foreach (var label in labels)
            {
                label.Text = "";
            }
        }

        private void btnSirala_Click(object sender, EventArgs e)
        {
            ListBox[] __sortingProjection = new ListBox[]
            {
                /* [listBox1] = */ listBox12,
                /* [listBox2] = */ listBox11,
                /* [listBox3] = */ listBox10,
                /* [listBox4] = */ listBox9,
                /* [listBox5] = */ listBox8,
                /* [listBox6] = */ listBox7
            };

            int[][] sayilarKopya = new int[6][];
            __sayilar.CopyTo(sayilarKopya, 0);

            foreach (var array in sayilarKopya)
            {
                Array.Sort(array);
            }

            for (int i = 0; i < 6; i++)
            {
                __sortingProjection[i].Items.Clear();
                foreach (var item in sayilarKopya[i])
                {
                    __sortingProjection[i].Items.Add(item);
                }
            }
        }

        private void btnSayiCek_Click(object sender, EventArgs e)
        {
            int[] balls = new int[6];
            balls.StuffWithRandomNumbers(1, 20, unique: true);
            lblToplar.Text = balls.ToLineString("-");

            ListBox[] listBoxes = (from Control ctl in Controls where ctl is ListBox _ select (ListBox)ctl).ToArray();
            foreach (var currListBox in listBoxes)
            {
                List<int> ballIndices = new List<int>();
                for (int i = 0; i < 6; i++)
                {
                    List<int> indices = new List<int>();
                    for (int j = 0; j < currListBox.Items.Count; j++)
                    {
                        if((int)currListBox.Items[i] == balls[i])
                            indices.Add(j);
                    }
                    if (indices.Count > 0)
                        ballIndices.AddRange(indices);
                }
                ballIndices.TrimExcess();
                currListBox.SelectedIndices.Clear();
                foreach (var ballIndex in ballIndices)
                {
                    currListBox.SetSelected(ballIndex, true);
                }
            }

            int[] itemCounts = new int[6];
            for (int i = 0; i < 6; i++)
            {
                itemCounts[i] = listBoxes[i].SelectedIndices.Count;
            }

            Label[] labels = (from Control ctl in Controls where ctl is Label _ && ctl.Name != "lblToplar" select (Label)ctl).ToArray();

            for (int i = 0; i < 6; i++)
            {
                labels[i].Text = itemCounts[i].ToString();
            }
        }
    }
}
