using System.Windows.Threading;
using Isolib.IOPackage;
using Isolib.STFSPackage;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BladestormSE
{
    public partial class MainWindow
    {
        #region Variables

        private string _filepath;
        private Stfs _stfs;
        private byte[] buffer;
        private long _offset;

        #endregion Variables

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Input

        private void OpenFile()
        {
            try
            {
                var open = new OpenFileDialog() { Title = "Open Save", Filter = "BLADESTORM_*|*BLADESTORM_EU;*BLADESTORM_NA" };

                SetStatus("Opening Save");
                open.ShowDialog();
                _filepath = open.FileName;
                SetStatus("Scanning... Please Wait!");
                if (_filepath == null)
                {
                    SetStatus("Idle");
                    return;
                    //abort load
                }
                SetStatus(("Reading: " + open.SafeFileName));

                _stfs = new Stfs(_filepath);
                buffer = _stfs.Extract(0);
                Task.Factory.StartNew(ReadFile);
                Task.Factory.StartNew(ReadID);
                SetStatus("Loaded!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void ReadFile()
        {
            int[] offsets =
            {
                0xE35800,
                0xE35830,
                0xE35850
            };
            var usedslots = new bool[30];
            using (var reader = new Reader(buffer, true))
            {
                var constructor = new StringBuilder();

                //First We determine used slots.
                for (var i = 0; i < 30; i++)
                {
                    //Get Locale/Used Slot
                    reader.Position = offsets[0];
                    string storage = (reader.ReadString(StringType.Ascii, 16));
                    storage = storage.Trim(Convert.ToChar("\0"));
                    if (storage == "NODATA")
                    {
                        //Flag slot as unused
                        usedslots[i] = false;
                        //add it to the list.
                        constructor.Append(storage);
                        return;
                    }
                    usedslots[i] = true;
                    constructor.Append(storage);

                    //Get Time
                    constructor.Append(", ");
                    reader.Position = offsets[1];
                    storage = (reader.ReadString(StringType.Ascii, 16));
                    storage = storage.Trim(Convert.ToChar("\0"));
                    constructor.Append(storage);
                    //Get Name
                    constructor.Append(", ");
                    reader.Position = offsets[2];
                    storage = (reader.ReadString(StringType.Ascii, 16));
                    storage = storage.Trim(Convert.ToChar("\0"));
                    constructor.Append(storage);
                    //now containts TAVERN, [TIME], Character Name
                     SaveSlot.Dispatcher.Invoke(new Action(() => SaveSlot.Items.Add(constructor.ToString())));
               constructor.Clear();
                    //refreshes constructor for next loop trip
                    offsets[0] += 128;
                    offsets[1] += 128;
                    offsets[2] += 128;
                }
                //TODO Check for empty save, message and return if so

                //End Read Slots
                SaveSlot.Dispatcher.Invoke(new Action(delegate { SaveSlot.SelectedIndex = 0; }));
                
                // sets the combobox up nicely otherwise it leaves a gap.
                SetStatus("Please Select A Valid Save Slot.");
            Retry:
                if (SaveSlot.SelectedItem.ToString().Contains("NODATA"))
                {
                    goto Retry;
                }
                _offset = (0x44 + (0x79400 * SaveSlot.SelectedIndex));
                try
                {
                    //name
                    reader.Position = _offset + 4;
                    //offset is now based upon chosen save slot
                    CharName.Text = reader.ReadString(StringType.Ascii, 16);
                    CharName.Text = CharName.Text.Trim(Convert.ToChar(""));

                    //Readoffsets(MoneyBox, 64, 4);
                    //Readoffsets(knivelevelbox, 842, 2);
                    //Readoffsets(knivepointbox, 848, 4);
                    //Readoffsets(rapierlevelbox, 926, 2);
                    //Readoffsets(rapierpointbox, 932, 4);
                    //Readoffsets(swordlevelbox, 1010, 2);
                    //Readoffsets(swordpointbox, 1016, 4);
                    //Readoffsets(spearlevelbox, 1094, 2);
                    //Readoffsets(spearpointbox, 1100, 4);
                    //Readoffsets(longspearlevelbox, 1178, 2);
                    //Readoffsets(longspearpointbox, 1184, 4);
                    //Readoffsets(horseslevelbox, 1262, 2);
                    //Readoffsets(horsespointbox, 1268, 4);
                    //Readoffsets(halberdslevelbox, 1346, 2);
                    //Readoffsets(halberdspointsbox, 1352, 4);
                    //Readoffsets(Axeslevelbox, 1430, 2);
                    //Readoffsets(axepointbox, 1436, 4);
                    //Readoffsets(Clubslevelboxes, 1514, 2);
                    //Readoffsets(clubpointbox, 1520, 4);
                    //Readoffsets(Bowlevelbox, 1598, 2);
                    //Readoffsets(bowpointbox, 1604, 4);
                    //Readoffsets(horsebowlevel, 1682, 2);
                    //Readoffsets(horsebowpointbox, 1688, 4);
                    //Readoffsets(Camellevelbox, 1766, 2);
                    //Readoffsets(camelpointbox, 1772, 4);
                    //Readoffsets(elephantlevelbox, 1850, 2);
                    //Readoffsets(elephantpointbox, 1856, 4);
                    //Readoffsets(chariotlevelbox, 1934, 2);
                    //Readoffsets(chariotpointbox, 1940, 4);
                    //Readoffsets(explosivelevelbox, 2018, 2);
                    //Readoffsets(explosivepointbox, 2024, 4);
                    //Readoffsets(magiclevelbox, 2102, 2);
                    //Readoffsets(magicpointbox, 2108, 4);
                    //Readoffsets(engineerlevelbox, 2186, 2);
                    //Readoffsets(engineerpointbox, 2192, 4);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                SetStatus("Loaded!");
                reader.Flush();
            }
        }

        public void ReadID()
        {
            //Profile ID
            PID.Text = _stfs.HeaderData.ProfileID;
            //Console ID
            CID.Text = _stfs.HeaderData.ConsoleID;
            //Title ID
            GID.Text = _stfs.HeaderData.TitleID.Substring(8);
            //Smallbug if not substringed
            //Device ID
            DID.Text = _stfs.HeaderData.DeviceID;
            //Save Name
            ContentBox.Text = _stfs.HeaderData.DisplayName;
        }

        #endregion Input

        #region Output

        #endregion Output

        #region Utilities

        private void MaxLevelBtnClick(object sender, RoutedEventArgs e)
        {
            {
                foreach (SquadControl squad in (Squads1.Children.OfType<SquadControl>()))
                {
                    squad.MaxLevel(null, null);
                }
                foreach (SquadControl squad in (Squads2.Children.OfType<SquadControl>()))
                {
                    squad.MaxLevel(null, null);
                }
                foreach (SquadControl squad in (Squads3.Children.OfType<SquadControl>()))
                {
                    squad.MaxLevel(null, null);
                }
            }
        }

        private void MaxPointsBtnClick(object sender, RoutedEventArgs e)
        {
            {
                foreach (SquadControl squad in (Squads1.Children.OfType<SquadControl>()))
                {
                    squad.MaxPoints(null, null);
                }
                foreach (SquadControl squad in (Squads2.Children.OfType<SquadControl>()))
                {
                    squad.MaxPoints(null, null);
                }
                foreach (SquadControl squad in (Squads3.Children.OfType<SquadControl>()))
                {
                    squad.MaxPoints(null, null);
                }
            }
        }

        private void MaxMoneyClick(object sender, RoutedEventArgs e)
        {
            MoneyBox.Value = 0x00FFFFFF;
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AllcheckClick(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox check in checkwrap.Children.OfType<CheckBox>())
            {
                check.IsChecked = check.IsChecked != true;
            }
        }

        #region id management

        private void NullidbtnClick(object sender, RoutedEventArgs e)
        {
            PID.Text = "E000000000000000";
            DID.Text = "0000000000000000000000000000000000000000";
            CID.Text = "0000000000";
        }

        private void RestoreidbtnClick(object sender, RoutedEventArgs e)
        {
            ReadID();
        }

        private void TransferidbtnClick(object sender, RoutedEventArgs e)
        {
        }

        #endregion id management

        private void SetStatus(string text)
        {
            if (status.Dispatcher.CheckAccess()) //statusstrip control has invoke, status does not.
                status.Content = ("Status: " + text);
            else
            {
                status.Dispatcher.Invoke(new Action(delegate { status.Content = ("Status: " + text); }));
            }
        }

        #endregion Utilities

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(OpenFile);
        }
    }
}