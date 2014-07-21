using BladestormSE.Resources;
using Isolib.IOPackage;
using Isolib.STFSPackage;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BladestormSE
{
    public partial class MainWindow
    {
        #region Variables

        private List<Slot> Slots = new List<Slot>();
        private string _filepath;
        private long _offset;
        private Stfs _stfs;
        private byte[] buffer;

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
                var open = new OpenFileDialog
                           {
                               Title = "Open Xbox360 Bladestorm Save",
                               Filter = "BLADESTORM_*|*BLADESTORM_EU;*BLADESTORM_NA",
                               CheckFileExists = true,
                               Multiselect = false
                           };
                SetStatus("Opening Save");
                //If user cancels or otherwise does not select a save, return.
                if ((bool)!open.ShowDialog())
                {
                    //abort load
                    SetStatus("Idle");
                    return;
                }
                _filepath = open.FileName;
                SetStatus(("Reading " + open.SafeFileName));

                _stfs = new Stfs(_filepath);
                buffer = _stfs.Extract(0);
                ReadID();
                ReadSlots();
                SetStatus("Loaded!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ReadSlots()
        {
            int[] offsets =
            {
                0xE35800,
                0xE35830,
                0xE35850
            };

            using (var reader = new Reader(buffer, true))
            {
                var constructor = new StringBuilder();
                for (int i = 0; i < 30; i++)
                {
                    SetStatus("Scanning Slot#" + (i + 1).ToString(CultureInfo.InvariantCulture));

                    //Pre-scan for name entries to map 'empty' slots. This accomodates foreign languages.
                    reader.Position = 0xE35850 + (128 * i);
                    if (reader.ReadInt8() == 0) continue;

                    constructor.Append("Slot#" + (i + 1));
                    //Get Locale, Time & Name
                    for (int o = 0; o < 3; o++)
                    {
                        constructor.Append(", ");
                        reader.Position = offsets[o] + (128 * i);
                        constructor.Append(reader.ReadString(StringType.Ascii, 16).Replace("\0", ""));// .Trim(Convert.ToChar("\0")));
                    }

                    //Build List

                    //SaveSlot.Items.Add(constructor.ToString());
                    Slots.Add(new Slot
                             {
                                 SlotString = constructor.ToString(),
                                 StartingOffset = 0x44 + (0x79400 * (i + 1)),
                                 ID = i
                             });
                    constructor.Clear();
                }
                //TODO Add flag for failed load, reset editor state.
                if (Slots.Count == 0)
                {
                    //If no slot is tagged as used, editing is pointless since there's nothing to work with.
                    MessageBox.Show(
                        "This save appears to be empty!\nIf this save does contain a used slot, an error has occurred.\nEither way, aborting load operation.",
                        "Error!", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }
                //Load first valid slot
                SaveSlot.SelectedIndex = 0;
                SaveSlot.SelectionChanged += LoadSlot;
            }
        }

        private void ReadSlot()
        {
            using (var reader = new Reader(buffer, true))
            {
                SetStatus("Reading Slot# " + (SaveSlot.SelectedIndex + 1));
                _offset = (0x44 + (0x79400 * SaveSlot.SelectedIndex));
                try
                {
                    //Name
                    SetStatus("Reading Name");
                    reader.Position = _offset + 4;
                    Slots[SaveSlot.SelectedIndex].Name =
                        reader.ReadString(StringType.Ascii, 16).Trim(Convert.ToChar("\0"));
                    // Slot[SaveSlot.SelectedIndex].Name = CharName.Text = reader.ReadString(StringType.Ascii, 16).Trim(Convert.ToChar("\0"));

                    //Money
                    SetStatus("Reading Money");
                    reader.Position = _offset + 64;
                    Slots[SaveSlot.SelectedIndex].Money = (int)(MoneyBox.Value = reader.ReadInt32());

                    //Squad Reads
                    int counter = 0;
                    foreach (Squad squad in Slots[SaveSlot.SelectedIndex].Squads)
                    {
                        SetStatus("Reading " + (Squaddies)counter);
                        reader.Position = _offset + squad.Adjust;
                        squad.Level = reader.ReadUInt16();
                        reader.Position += 4;
                        squad.Points = reader.ReadInt32();
                        counter++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    SetStatus("ERROR!");
                    return;
                }

                SetStatus("Loaded!");
                reader.Flush();
            }
        }

        private void LoadSlot(object sender, SelectionChangedEventArgs e)
        {
            ////Save
            //Slot[SaveSlot.SelectedIndex].Name = CharName.Text;
            //Slot[SaveSlot.SelectedIndex].Money = MoneyBox.Value;
            //Slot[SaveSlot.SelectedIndex].Knivelv = knives.Levelbox.Value;
            //Slot[SaveSlot.SelectedIndex].Knivepoint = knives.PointBox.Value;

            ////Load
            CharName.Text = Slots[SaveSlot.SelectedIndex].Name;
            //MoneyBox.Value = Slot[SaveSlot.SelectedIndex].Money;
            //knives.Levelbox.Value = (int?)Slots[SaveSlot.SelectedIndex].Knivelv;
            //knives.PointBox.Value = (int?)Slots[SaveSlot.SelectedIndex].Knivepoint;
            //swords.Levelbox.Value = (int?)Slots[SaveSlot.SelectedIndex].Swordlv;
            //swords.PointBox.Value = (int?)Slots[SaveSlot.SelectedIndex].Swordpoint;

            MoneyBox.DataContext = Slots[SaveSlot.SelectedIndex].Money;
        }

        public void ReadID()
        {
            //Profile ID
            PID.Dispatcher.Invoke(new Action(() => PID.Text = _stfs.HeaderData.ProfileID));

            //Console ID
            CID.Dispatcher.Invoke(new Action(() => CID.Text = _stfs.HeaderData.ConsoleID));

            //Title ID - Smallbug if not substringed
            GID.Dispatcher.Invoke(new Action(() => GID.Text = _stfs.HeaderData.TitleID.Substring(8)));

            //Device ID
            DID.Dispatcher.Invoke(new Action(() => DID.Text = _stfs.HeaderData.DeviceID));

            //Save Name
            ContentBox.Dispatcher.Invoke(new Action(() => ContentBox.Text = _stfs.HeaderData.DisplayName));
        }

        #endregion Input

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
                //A toggle switch
                check.IsChecked = check.IsChecked != true;
            }
        }

        private void SetStatus(string text)
        {
            if (status.Dispatcher.CheckAccess())
                status.Content = ("Status: " + text);
            else
            {
                status.Dispatcher.Invoke(delegate { status.Content = ("Status: " + text); });
            }
        }

        #region ID Management

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

        #endregion ID Management

        #endregion Utilities

        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    base.OnMouseLeftButtonDown(e);

        //    // Begin dragging the window
        //    DragMove();
        //}

        private void OpenButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }
    }
}