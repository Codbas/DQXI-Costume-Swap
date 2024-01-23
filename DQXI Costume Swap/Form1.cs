using DQXICostumeSwap.Resources;
using DQXICostumeSwap.Utilities;
using Memory.Utils;
using Memory.Win64;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DQXICostumeSwap
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            SetComboBoxDefaults();
            infoLabel.Text = "";


            var MyIni = new IniFile();
            string themeCheck = MyIni.Read("DarkTheme", "Theme");
            if (themeCheck == "true")
            {
                ApplyDarkTheme();
                themeCheckBox.Checked = true;
            }
            else
            {
                ApplyLightTheme();
                themeCheckBox.Checked = false;
            }

        }

        public static ulong[] pointer = new ulong[32];

        ulong[] baseAddress = new ulong[32]
            {//   Body        Head        Face      Base Mesh
                0x04126E48, 0x040F2CE8, 0x040E8088, 0x040DA538, // Luminary | P001
                0x040E52C8, 0x040FFC50, 0x040BF190, 0x040C1F38, // Erik     | P002
                0x040EA410, 0x04131218, 0x040D4168, 0x040F4FC8, // Veronica | P003
                0x040FBD28, 0x040D45A8, 0x040FF190, 0x04109A28, // Serena   | P004
                0x04123DA0, 0x040B8B50, 0x040FE598, 0x0410E158, // Sylvando | P005
                0x040F20B0, 0x04122D98, 0x0410DB38, 0x0410FDE8, // Jade     | P006
                0x0411C760, 0x040FB4B8, 0x040E32D8, 0x040EF1F0, // Rab      | P007
                0x0412BF20, 0x04123358, 0x040F3418, 0x040CE2D8  // Hendrik  | P008
            };

        int[][] offsets = new int[32][]
            // Body 
            // Head 
            // Face 
            // Base 

        {   // Luminary  | P001
            new int[1] { 0x10 },
            new int[4] { 0x8, 0x8, 0x70, 0x638 },
            new int[4] { 0x8, 0x8, 0x48, 0x328 },
            new int[1] { 0x108 },

            // Erik     | P002
            new int [1] { 0x638 },
            new int [1] { 0x480 },
            new int [1] { 0x88 },
            new int [1] { 0xA0 },

            // Veronica | P003
            new int[5] { 0xA0, 0x30, 0x8, 0x58, 0x6E8 },
            new int[5] { 0x80, 0x8, 0x70, 0x38, 0x538 },
            new int[1] { 0x4F8 },
            new int[1] { 0x98 },
            
            // Serena   | P004
            new int[3] { 0x8, 0x90, 0x10 },
            new int[1] { 0x1D0 },
            new int[1] { 0x520 },
            new int[1] { 0x108 },
            
            // Sylvando | P005
            new int[1] { 0x2C0 },
            new int[1] { 0x358 },
            new int[3] { 0x8, 0xF8, 0x7F0 },
            new int[1] { 0x98 },
            
            // Jade     | P006
            new int [1] { 0x530 },
            new int [1] { 0x448 },
            new int [5] { 0x30, 0x90, 0x8, 0xC0, 0x678 },
            new int [1] { 0x40 },
            
            // Rab      | P007
            new int [5] { 0x8, 0x8, 0x8, 0xA0, 0x178 },
            new int [5] { 0x8, 0x8, 0x8, 0x8, 0x7D8 },
            new int [2] { 0x8, 0x358 },
            new int [1] { 0x130 }, 

            // Hendrik  | P008
            new int[5] { 0x8, 0x8, 0x8, 0xB0, 0x118 },
            new int[2] { 0x78, 0x778 },
            new int[5] { 0x78, 0x8, 0x8, 0xC8, 0x4D0 },
            new int[1] { 0x10 }
        };

        MemoryHelper64 helper;
        GetName compare;
        Character s = new Character();
        bool hasUnchecked = false;
        bool go = true;

        private void ApplyDarkTheme()
        {
            BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            foreach (Label l in Controls.OfType<Label>())
                l.ForeColor = Color.White;

            themeCheckBox.ForeColor = Color.White;
            advancedCheckBox.ForeColor = Color.White;
            npcCheckBox.ForeColor = Color.White;

            applyButton.BackColor = Color.FromArgb(80, 80, 80);
            applyButton.ForeColor = Color.White;

            saveButton.BackColor = Color.FromArgb(80, 80, 80);
            saveButton.ForeColor = Color.White;

            loadButton.BackColor = Color.FromArgb(80, 80, 80);
            loadButton.ForeColor = Color.White;

            foreach (ComboBox c in Controls.OfType<ComboBox>())
            {
                c.ForeColor = Color.White;
                c.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void ApplyLightTheme()
        {
            BackColor = System.Drawing.Color.White;
            foreach (Label l in Controls.OfType<Label>())
                l.ForeColor = Color.Black;

            themeCheckBox.ForeColor = Color.Black;
            advancedCheckBox.ForeColor = Color.Black;
            npcCheckBox.ForeColor = Color.Black;

            applyButton.BackColor = Color.White;
            applyButton.ForeColor = Color.Black;

            saveButton.BackColor = Color.White;
            saveButton.ForeColor = Color.Black;

            loadButton.BackColor = Color.White;
            loadButton.ForeColor = Color.Black;

            foreach (ComboBox c in Controls.OfType<ComboBox>())
            {
                c.ForeColor = Color.Black;
                c.BackColor = Color.FromArgb(200, 200, 200);
            }
        }

        private void SetComboBoxDefaults()
        {
            luminaryBodyComboBox.Items.AddRange(s.luminaryBodyList);
            luminaryHeadComboBox.Items.AddRange(s.luminaryHeadList);

            erikBodyComboBox.Items.AddRange(s.erikBodyList);
            erikHeadComboBox.Items.AddRange(s.erikHeadList);

            veronicaBodyComboBox.Items.AddRange(s.veronicaBodyList);
            veronicaHeadComboBox.Items.AddRange(s.veronicaHeadList);

            serenaBodyComboBox.Items.AddRange(s.serenaBodyList);
            serenaHeadComboBox.Items.AddRange(s.serenaHeadList);

            sylvandoBodyComboBox.Items.AddRange(s.sylvandoBodyList);
            sylvandoHeadComboBox.Items.AddRange(s.sylvandoHeadList);

            jadeBodyComboBox.Items.AddRange(s.jadeBodyList);
            jadeHeadComboBox.Items.AddRange(s.jadeHeadList);

            rabBodyComboBox.Items.AddRange(s.rabBodyList);
            rabHeadComboBox.Items.AddRange(s.rabHeadList);

            hendrikBodyComboBox.Items.AddRange(s.hendrikBodyList);
            hendrikHeadComboBox.Items.AddRange(s.hendrikHeadList);

            int defaultIndex = 0;
            int meshType = 0;

            foreach (Control cnl in Controls)
            {
                if (cnl is ComboBox)
                {
                    ComboBox cb = (ComboBox)cnl;

                    (defaultIndex, meshType) = s.GetTheStuff(cb.Name, out int blah);

                    switch (meshType)
                    {
                        case 0: // Body Mesh
                            {
                                cb.SelectedIndex = 0;
                                break;
                            }
                        case 1: // Head Mesh
                            {
                                cb.SelectedIndex = 0;
                                break;
                            }
                        case 2: // Face Mesh
                            {
                                cb.Items.AddRange(s.faceMeshList);
                                cb.SelectedIndex = defaultIndex;
                                cb.Enabled = false;
                                break;
                            }
                        case 3: //Base Mesh
                            {
                                cb.Items.AddRange(s.baseMeshList);
                                cb.SelectedIndex = defaultIndex;
                                cb.Enabled = false;
                                break;
                            }
                        default:
                            {
                                // Shouldn't happen
                                MessageBox.Show("Error getting meshType in SetComboBoxDefaults()");
                                break;
                            }
                    }
                }
            }
        }

        private void GetPointers()
        {
            for (int i = 0; i < 32; i++)
            {
                pointer[i] = MemoryUtils.OffsetCalculator(helper, helper.GetBaseAddress(baseAddress[i]), offsets[i]);
            }
        }

        private void WriteAddress()
        {
            string writeValue = "";   // /Game...
            int writeType = 0;  // 1 = Base, 2 = Party/NPC
            ulong target;

            foreach (Control cnl in Controls)
            {
                if (cnl is ComboBox)
                {
                    ComboBox cb = (ComboBox)cnl;
                    if (!cb.Enabled) { /*do nothing */ }
                    else
                    {
                        if (advancedCheckBox.Checked || npcCheckBox.Checked)
                        {
                            writeType = 2;
                        }
                        else
                        {
                            writeType = 1;
                        }

                        (target, writeValue) = s.GetInfo(cb.Name, cb.SelectedIndex, writeType);

                        /*Need to get writeValue
                         * key = cb.Name 
                         * key = "luminaryBodyComboBox"
                         * 
                         * 
                         * 2 different dict -> NPC/Party & Base
                         * if NPC/Party
                         * string[] value = lookup[cb.Name]
                         * writeValue = value[cb.SelectedIndex]
                         * 
                         */

                        helper.WriteMemory<String>(target, writeValue, writeValue.Length);
                    }
                }
            }
        }

        private void WriteAddressBase()
        {
            helper.WriteMemory<String>(pointer[0], s.luminaryBodyArr[luminaryBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[1], s.luminaryHeadArr[luminaryHeadComboBox.SelectedIndex], 79);

            helper.WriteMemory<String>(pointer[4], s.erikBodyArr[erikBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[5], s.erikHeadArr[erikHeadComboBox.SelectedIndex], 79);

            helper.WriteMemory<String>(pointer[8], s.veronicaBodyArr[veronicaBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[9], s.veronicaHeadArr[veronicaHeadComboBox.SelectedIndex], 79);

            helper.WriteMemory<String>(pointer[12], s.serenaBodyArr[serenaBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[13], s.serenaHeadArr[serenaHeadComboBox.SelectedIndex], 79);

            helper.WriteMemory<String>(pointer[16], s.sylvandoBodyArr[sylvandoBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[17], s.sylvandoHeadArr[sylvandoHeadComboBox.SelectedIndex], 79);

            helper.WriteMemory<String>(pointer[20], s.jadeBodyArr[jadeBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[21], s.jadeHeadArr[jadeHeadComboBox.SelectedIndex], 79);

            helper.WriteMemory<String>(pointer[24], s.rabBodyArr[rabBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[25], s.rabHeadArr[rabHeadComboBox.SelectedIndex], 79);

            helper.WriteMemory<String>(pointer[28], s.hendrikBodyArr[hendrikBodyComboBox.SelectedIndex], 73);
            helper.WriteMemory<String>(pointer[29], s.hendrikHeadArr[hendrikHeadComboBox.SelectedIndex], 79);
        }

        private void WriteAddressesUnchecked()
        {
            ulong target;
            string writeValue;
            int writeType = 1; //Base 
            foreach (Control cnl in Controls)
            {
                if (cnl is ComboBox)
                {
                    ComboBox cb = (ComboBox)cnl;
                    if (cb.Enabled) { /*do nothing */ }
                    else
                    {
                        (target, writeValue) = s.GetInfo(cb.Name, cb.SelectedIndex, writeType);

                        helper.WriteMemory<String>(target, writeValue, writeValue.Length);
                    }
                }
            }
        }

        private void CheckBoxEnabled()
        {
            int defaultIndex = 0;
            int meshType = 0;

            foreach (Control cnl in Controls)
            {
                if (cnl is ComboBox)
                {
                    ComboBox cb = (ComboBox)cnl;
                    if (!cb.Enabled) { cb.Enabled = true; }

                    cb.Items.Clear();
                    (defaultIndex, meshType) = s.GetTheStuff(cb.Name, out int blah);

                    if (!npcCheckBox.Checked)
                    {
                        switch (meshType)
                        {
                            case 0: // Body Mesh
                                {
                                    cb.Items.AddRange(s.allBodyList);
                                    break;
                                }
                            case 1: // Head Mesh
                                {
                                    cb.Items.AddRange(s.allHeadList);
                                    break;
                                }
                            case 2: // Face Mesh
                                {
                                    cb.Items.AddRange(s.faceMeshList);
                                    cb.Enabled = true;
                                    break;
                                }
                            case 3: //Base Mesh
                                {
                                    cb.Items.AddRange(s.baseMeshList);
                                    cb.Enabled = true;
                                    break;
                                }
                            default:
                                {
                                    // Shouldn't happen
                                    MessageBox.Show("Error getting meshType in CheckBoxEnabled()");
                                    break;
                                }
                        }
                    }

                    // NPC check box checked
                    else
                    {
                        switch (meshType)
                        {
                            case 0: //Body Mesh
                                {
                                    cb.Items.AddRange(s.npcAllBodyList);
                                    break;
                                }
                            case 1: //Head Mesh
                                {
                                    cb.Items.AddRange(s.npcAllHeadList);
                                    break;
                                }
                            case 2: //Face Mesh
                                {
                                    cb.Items.AddRange(s.npcFaceMeshList);
                                    cb.Enabled = true;
                                    break;
                                }
                            case 3: //Base Mesh
                                {
                                    cb.Items.AddRange(s.npcBaseMeshList);
                                    cb.Enabled = true;
                                    break;
                                }
                            default: //shouldn't happen
                                {
                                    break;
                                }
                        }
                    }

                    cb.SelectedIndex = defaultIndex;
                }
            }
        }

        private void CheckBoxDisabled()
        {
            int defaultIndex = 0;
            int meshType = 0;

            foreach (Control cnl in Controls)
            {
                if (cnl is ComboBox)
                {
                    ComboBox cb = (ComboBox)cnl;

                    cb.Items.Clear();

                    (defaultIndex, meshType) = s.GetTheStuff(cb.Name, out int blah);

                    switch (meshType)
                    {
                        case 0: { break; } // Body Mesh
                        case 1: { break; } // Head Mesh

                        case 2: // Face Mesh
                            {
                                cb.Items.AddRange(s.faceMeshList);
                                cb.Enabled = false;
                                cb.SelectedIndex = defaultIndex;
                                break;
                            }
                        case 3: //Base Mesh
                            {
                                cb.Items.AddRange(s.baseMeshList);
                                cb.Enabled = false;
                                cb.SelectedIndex = defaultIndex;
                                break;
                            }
                        default:
                            {
                                // Shouldn't happen
                                MessageBox.Show("Error getting meshType in CheckBoxDisabled()");
                                break;
                            }
                    }
                }
            }

            luminaryBodyComboBox.Items.AddRange(s.luminaryBodyList);
            luminaryBodyComboBox.SelectedIndex = 0;
            luminaryHeadComboBox.Items.AddRange(s.luminaryHeadList);
            luminaryHeadComboBox.SelectedIndex = 0;

            erikBodyComboBox.Items.AddRange(s.erikBodyList);
            erikBodyComboBox.SelectedIndex = 0;
            erikHeadComboBox.Items.AddRange(s.erikHeadList);
            erikHeadComboBox.SelectedIndex = 0;

            veronicaBodyComboBox.Items.AddRange(s.veronicaBodyList);
            veronicaBodyComboBox.SelectedIndex = 0;
            veronicaHeadComboBox.Items.AddRange(s.veronicaHeadList);
            veronicaHeadComboBox.SelectedIndex = 0;

            serenaBodyComboBox.Items.AddRange(s.serenaBodyList);
            serenaBodyComboBox.SelectedIndex = 0;
            serenaHeadComboBox.Items.AddRange(s.serenaHeadList);
            serenaHeadComboBox.SelectedIndex = 0;

            sylvandoBodyComboBox.Items.AddRange(s.sylvandoBodyList);
            sylvandoBodyComboBox.SelectedIndex = 0;
            sylvandoHeadComboBox.Items.AddRange(s.sylvandoHeadList);
            sylvandoHeadComboBox.SelectedIndex = 0;

            jadeBodyComboBox.Items.AddRange(s.jadeBodyList);
            jadeBodyComboBox.SelectedIndex = 0;
            jadeHeadComboBox.Items.AddRange(s.jadeHeadList);
            jadeHeadComboBox.SelectedIndex = 0;

            rabBodyComboBox.Items.AddRange(s.rabBodyList);
            rabBodyComboBox.SelectedIndex = 0;
            rabHeadComboBox.Items.AddRange(s.rabHeadList);
            rabHeadComboBox.SelectedIndex = 0;

            hendrikBodyComboBox.Items.AddRange(s.hendrikBodyList);
            hendrikBodyComboBox.SelectedIndex = 0;
            hendrikHeadComboBox.Items.AddRange(s.hendrikHeadList);
            hendrikHeadComboBox.SelectedIndex = 0;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            Process p = Process.GetProcessesByName("DRAGON QUEST XI").ToList().FirstOrDefault();

            if (p == null)
            {
                infoLabel.Text = "Could not find DQXI. Is the game running?";
            }

            else //only run if DQXI is running
            {
                helper = new MemoryHelper64(p);

                if (helper == null) // game did not hook for some reason
                {
                    MessageBox.Show("Error hooking DQXI process");
                }

                else
                {
                    GetPointers();

                    //Check to see if pointers will find correct addresses
                    byte[] data = helper.ReadMemoryBytes(pointer[0], 23);
                    string result = Encoding.ASCII.GetString(data);
                    if (result != "/Game/Characters/Human/")
                    {
                        MessageBox.Show("DQXI Version not compatable!");
                        infoLabel.Text = "";
                    }
                    else
                    {
                        if (advancedCheckBox.Checked || npcCheckBox.Checked)
                        {
                            WriteAddress();
                        }
                        else
                        {
                            WriteAddressBase();
                        }
                        //apply default face/base meshes
                        if (hasUnchecked)
                        {
                            WriteAddressesUnchecked();
                            hasUnchecked = false;
                        }
                        infoLabel.Text = $"Costumes successfully swappwed.";
                    }
                }
            }
        }

        public void advancedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (advancedCheckBox.Checked)
            { //enable face and body comboBoxes
                CheckBoxEnabled();
                go = false;
            }
            else
            { //disable face and body comboBoxes
                go = true;
                hasUnchecked = true;
                npcCheckBox.Checked = false;

                CheckBoxDisabled();
            }
        }
        private void themeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Dark theme
            if (themeCheckBox.Checked)
            {
                ApplyDarkTheme();
                var f = new IniFile();
                f.Write("DarkTheme", "true", "Theme");
            }
            //Light Theme
            else
            {
                ApplyLightTheme();
                var f = new IniFile();
                f.Write("DarkTheme", "false", "Theme");
            }
        }

        public void npcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (npcCheckBox.Checked)
            {
                if (!advancedCheckBox.Checked)
                {
                    infoLabel.Text = "Must enable Party member swap to enable NPC swap!";
                    npcCheckBox.Checked = false;
                }
                else
                {
                    CheckBoxEnabled();
                }
            }
            else
            {
                if (!go)
                {
                    CheckBoxEnabled();
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            compare = new GetName();
            var f = new IniFile();
            string name;
            int writeType;
            string type;

            if (advancedCheckBox.Checked)
            {
                if (npcCheckBox.Checked)
                {
                    type = "NPC";
                }
                else
                {
                    type = "Party";
                }
                writeType = 32;
            }
            else
            {
                writeType = 16;
                type = "Base";
            }

            foreach (Control cnl in Controls)
            {
                if (cnl is ComboBox)
                {
                    ComboBox cb = (ComboBox)cnl;
                    if (!cb.Enabled) { /* Do nothing */ }
                    else
                    {
                        name = compare.CBtoNames(cb.Name, writeType);
                        f.Write($"{name}", $"{cb.SelectedIndex}", $"{type} Costume Swap Save");
                    }
                }
            }
            infoLabel.Text = $"Saved {type} costumes";
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            compare = new GetName();
            var f = new IniFile();
            string name;
            int writeType;
            string type;
            bool p;

            if (advancedCheckBox.Checked)
            {
                if (npcCheckBox.Checked)
                {
                    type = "NPC";
                }
                else
                {
                    type = "Party";
                }
                writeType = 32;
            }
            else
            {
                writeType = 16;
                type = "Base";
            }

            if (!f.KeyExists("Hendrik Head", $"{type} Costume Swap Save")) { infoLabel.Text = $"No save data for {type} Costumes detected."; }
            else
            {
                foreach (Control cnl in Controls)
                {
                    if (cnl is ComboBox)
                    {
                        ComboBox cb = (ComboBox)cnl;
                        if (!cb.Enabled) { /* Do nothing */ }
                        else
                        {
                            name = compare.CBtoNames(cb.Name, writeType);
                            p = Int32.TryParse(f.Read($"{name}", $"{type} Costume Swap Save"), out int a);
                            if (p)
                            {
                                cb.SelectedIndex = a;
                            }
                            else
                            {
                                infoLabel.Text = ($"Error loading {type} settings!\nUnable to parse file.");
                            }
                        }
                    }
                }
                infoLabel.Text = $"Loaded {type} costumes";
            }
        }


        




        Dictionary<string, List<string>> bodyDict;
        List<string> cbNames = new List<string>
        {
            "luminaryBodyComboBox",
            "luminaryHeadComboBox",
            "luminaryFaceComboBox",
            "luminaryBaseMeshComboBox",
            "erikBodyComboBox",
            "erikHeadComboBox",
            "erikFaceComboBox",
            "erikBaseMeshComboBox",
            "veronicaBodyComboBox",
            "veronicaHeadComboBox",
            "veronicaFaceComboBox",
            "veronicaBaseMeshComboBox",
            "serenaBodyComboBox",
            "serenaHeadComboBox",
            "serenaFaceComboBox",
            "serenaBaseMeshComboBox",
            "sylvandoBodyComboBox",
            "sylvandoHeadComboBox",
            "sylvandoFaceComboBox",
            "sylvandoBaseMeshComboBox",
            "jadeBodyComboBox",
            "jadeHeadComboBox",
            "jadeFaceComboBox",
            "jadeBaseMeshComboBox",
            "rabBodyComboBox",
            "rabHeadComboBox",
            "rabFaceComboBox",
            "rabBaseMeshComboBox",
            "hendrikBodyComboBox",
            "hendrikHeadComboBox",
            "hendrikFaceComboBox",
            "hendrikBaseMeshComboBox"
        };

        List<string> luminaryBodyList = new List<string>
        {
           "Default",
           "Cobblestone Clobber",
           "Hot Spring Clothes",
           "Gallopitan Garb",
           "Erdwin's Coronet and Tunic",
           "Mardi Garb",
           "Drasilian Armour",
           "Artisanal Trodain Togs"
        };

        List<string> luminaryHeadList = new List<string>
        {
            "Default",
            " ",
            "Erdwin's Coronet",
            "Drasilian Helm",
            "Artisanal Trodian Bandana",
            "Octagonia Tournament Mask",
            "Luminary - Gallopolis Race Helmet (Use with Gallopolis Face Mesh!)",
        };

        List<string> erikBodyList = new List<string>
        {
           "Default",
           "Bald [Hood Up]",
           "Hot Spring Clothes",
           "Pirate King's Coat",
           "Swindler King's Stole",
           "Viking Clothes"
        };

    }
    
}