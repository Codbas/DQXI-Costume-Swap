using DQXICostumeSwap;
using DQXICostumeSwap.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DQXICostumeSwap.Resources
{
    public class Character
        {
        public int[] defaultOffsets = new int[]
        {
        // Body | Head | Face | Base Mesh
            0,     0,      0,     0 , // Luminary | P001
            8,     7,      2,     1 , // Erik     | P002
            14,    11,     3,     2 , // Veronica | P003
            20,    17,     4,     3 , // Serena   | P004
            26,    23,     5,     4 , // Sylvando | P005
            29,    26,     6,     5 , // Jade     | P006
            36,    31,     8,     6 , // Rab      | P007
            38,    34,     9,     7   // Hendrik  | P008 

        };

        // Takes in combobox name
        // returns int defaultoffset, int meshType 
        // GetTheStuff(serenaBodyComboBox)
        // return defaultIndex = 20 (serena body default in NPC/all list), meshType = 0 (body)
        public (int, int) GetTheStuff(string ComboBoxName, out int ComboBoxIndex)
        {
            int i = 0;
            int index = 0;
            int DefaultIndex = 0;
            int MeshType = 0;

            //Find index to determine which party member & mesh type are being edited
            while (i < 32)
            {
                if (String.Equals(cbNames[i], ComboBoxName))
                {
                    i = 32;
                }
                else
                {
                    index++;
                }
                i++;
            }

            // 0: Body, 1: Head, 2: Face, 3: Base
            MeshType = index;
            while (MeshType > 3)
            {
                MeshType = MeshType - 4;
            }

            DefaultIndex = defaultOffsets[index];
            ComboBoxIndex = index;

            return (DefaultIndex, MeshType);
        }


        //WriteType -> 1 = Base, 2 = Party/NPC
        public (ulong, string) GetInfo(string ComboBoxName, int SelectedIndex, int WriteType)
        {
            int defaultIndex = 0; //Determines party member & mesh value
            int index = 0;
            int meshType;  //Body | Head | Face | Base
            int d = 0;      //index value for array after offset adjustment
            ulong memoryAddress = 0x0;
            string writeValue = "";

            (defaultIndex, meshType) = GetTheStuff(ComboBoxName, out index);

            memoryAddress = Main.pointer[index];

            switch (WriteType)
            {
                case 1: // Base
                    {
                        if (meshType == 0 || meshType == 1)
                        {
                            d = SelectedIndex;
                        }
                        else
                        {
                            d = defaultIndex;
                        }
                        break;
                    }
                case 2: // Party/NPC
                    {
                        d = SelectedIndex;
                        break;
                    }
                default: // shouldn't happen
                    {
                        MessageBox.Show("Error getting WriteType in GetInfo()");
                        break;
                    }
            }


            switch (meshType)
            {
                case 0: //Body Mesh
                    {
                        writeValue = npcAllBodyArr[d];
                        break;
                    }
                case 1: //Head Mesh
                    {
                        writeValue = npcAllHeadArr[d];
                        break;
                    }
                case 2: //Face Mesh
                    {
                        writeValue = npcFaceMeshArr[d];
                        break;
                    }
                case 3: //Base Mesh
                    {
                        writeValue = npcBaseMeshArr[d];
                        break;
                    }
                 default: //shouldn't happen
                    {
                        MessageBox.Show("Error getting meshType in GetInfo()");
                        break;
                    }
            }
            return (memoryAddress, writeValue);
        }

        public string[] cbNames = new string[]
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

        //Luminary
        public string[] luminaryBodyArr = new string[]
        {
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E001.SK_P001_Wear_E001",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E002.SK_P001_Wear_E002",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E004.SK_P001_Wear_E004",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E005.SK_P001_Wear_E005",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E007.SK_P001_Wear_E007",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E008.SK_P001_Wear_E008",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E009.SK_P001_Wear_E009",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E010.SK_P001_Wear_E010"
        };
        public string[] luminaryBodyList = new string[]
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
        public string[] luminaryHeadArr = new string[]
        {
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E001.SK_P001_Hair001_E001",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E003.SK_P001_Hair001_E003",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E007.SK_P001_Hair001_E007",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E009.SK_P001_Hair001_E009",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E010.SK_P001_Hair001_E010",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E011.SK_P001_Hair001_E011",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair001_E001.SK_N101_Hair001_E001",
        };
        public string[] luminaryHeadList = new string[]
        {
            "Default",
            " ",
            "Erdwin's Coronet",
            "Drasilian Helm",
            "Artisanal Trodian Bandana",
            "Octagonia Tournament Mask",
            "Luminary - Gallopolis Race Helmet (Use with Gallopolis Face Mesh!)",
        };
        //Eric
        public string[] erikBodyArr = new string[]
        {
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E001.SK_P002_Wear_E001",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E002.SK_P002_Wear_E002",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E003.SK_P002_Wear_E003",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E004.SK_P002_Wear_E004",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E005.SK_P002_Wear_E005",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E007.SK_P002_Wear_E007"
        };
        public string[] erikBodyList = new string[]
        {
           "Default",
           "Bald [Hood Up]",
           "Hot Spring Clothes",
           "Pirate King's Coat",
           "Swindler King's Stole",
           "Viking Clothes"
        };
        public string[] erikHeadArr = new string[]
        {
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E001.SK_P002_Hair001_E001",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E002.SK_P002_Hair001_E002",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E004.SK_P002_Hair001_E004",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E005.SK_P002_Hair001_E005"
        };
        public string[] erikHeadList = new string[]
       {
            "Default",
            "Hood Up",
            "Pirate King's Cap",
            "Swindler's Scarf"
       };
        //Veronica
        public string[] veronicaBodyArr = new string[]
        {
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E001.SK_P003_Wear_E001",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E002.SK_P003_Wear_E002",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E003.SK_P003_Wear_E003",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E004.SK_P003_Wear_E004",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E005.SK_P003_Wear_E005",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E006.SK_P003_Wear_E006"
        };
        public string[] veronicaBodyList = new string[]
        {
           "Default",
           "Gown of Eternity",
           "Stellar Suit",
           "Cat Suit",
           "Pretty Pinny",
           "Uniform de l'Academie"
        };
        public string[] veronicaHeadArr = new string[]
        {
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E001.SK_P003_Hair001_E001",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E002.SK_P003_Hair001_E002",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E003.SK_P003_Hair001_E003",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E004.SK_P003_Hair001_E004",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E005.SK_P003_Hair001_E005",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E006.SK_P003_Hair001_E006"
        };
        public string[] veronicaHeadList = new string[]
        {
            "Default",
            "Crown of Eternity",
            "Stellar Starflower",
            "Cat Hat",
            "Cute Cap",
            "Uniform de l'Academie [Hat]"
        };
        //Serena
        public string[] serenaBodyArr = new string[]
        {
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E001.SK_P004_Wear_E001",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E002.SK_P004_Wear_E002",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E003.SK_P004_Wear_E003",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E004.SK_P004_Wear_E004",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E005.SK_P004_Wear_E005",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E006.SK_P004_Wear_E006"
        };
        public string[] serenaBodyList = new string[]
        {
           "Default",
           "Serenica's Surplice",
           "Sacred Raiment",
           "Dancer's Costume",
           "Dainty Dress",
           "Uniform de l'Academie"
        };
        public string[] serenaHeadArr = new string[]
        {
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E001.SK_P004_Hair001_E001",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E003.SK_P004_Hair001_E003",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E004.SK_P004_Hair001_E004",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E007.SK_P004_Hair001_E007",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E009.SK_P004_Hair001_E009",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E012.SK_P004_Hair001_E012"
        };
        public string[] serenaHeadList = new string[]
        {
            "Default [Long]",
            "Serenica's Circlet [Long]",
            "Dancer's Costume [Long]",
            "Default [Short]",
            "Dancer's Costume [Short]",
            "Sacred Circlet [Short]"
        };
        //Sylvando
        public string[] sylvandoBodyArr = new string[]
        {
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E001.SK_P005_Wear_E001",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E003.SK_P005_Wear_E003",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E004.SK_P005_Wear_E004"
        };
        public string[] sylvandoBodyList = new string[]
        {
           "Default",
           "Glad Rags",
           "Dapper Doublet"
        };
        public string[] sylvandoHeadArr = new string[]
        {
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E001.SK_P005_Hair001_E001",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E002.SK_P005_Hair001_E002",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E004.SK_P005_Hair001_E004"
        };
        public string[] sylvandoHeadList = new string[]
        {
            "Default",
            "Mardi Garb",
            "Head-Turner"
        };
        //Jade
        public string[] jadeBodyArr = new string[]
        {
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E001.SK_P006_Wear_E001",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E002.SK_P006_Wear_E002",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E003.SK_P006_Wear_E003",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E004.SK_P006_Wear_E004",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E005.SK_P006_Wear_E005",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E006.SK_P006_Wear_E006",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E007.SK_P006_Wear_E007"
        };
        public string[] jadeBodyList = new string[]
        {
           "Default",
           "Xenlon Gown",
           "Minerva's Raiment",
           "Divine Bustier",
           "Scandalous Swimsuit",
           "Uniform de l'Academie",
           "Bunny Suit"
        };
        public string[] jadeHeadArr = new string[]
        {
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E001.SK_P006_Hair001_E001",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E002.SK_P006_Hair001_E002",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E003.SK_P006_Hair001_E003",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E004.SK_P006_Hair001_E004",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E007.SK_P006_Hair001_E007"
        };
        public string[] jadeHeadList = new string[]
        {
            "Default",
            "Xenlon Hair Ring",
            "Minerva's Tiara",
            "Divine Bustier [Flower]",
            "Bunny Ears"
        };
        //Rab
        public string[] rabBodyArr = new string[]
        {
            "/Game/Characters/Human/P007/Wear/Mesh/SK_P007_Wear_E001.SK_P007_Wear_E001",
            "/Game/Characters/Human/P007/Wear/Mesh/SK_P007_Wear_E002.SK_P007_Wear_E002"
        };
        public string[] rabBodyList = new string[]
      {
           "Default",
           "Pallium Regale"
      };
        public string[] rabHeadArr = new string[]
        {
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E001.SK_P007_Hair001_E001",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E002.SK_P007_Hair001_E002",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E003.SK_P007_Hair001_E003"
        };
        public string[] rabHeadList = new string[]
        {
            "Default",
            "Sun Crown",
            "Yggdrasil Crown"
        };
        //Hendrik
        public string[] hendrikBodyArr = new string[]
        {
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E002.SK_P008_Wear_E002",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E001.SK_P008_Wear_E001",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E003.SK_P008_Wear_E003",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E004.SK_P008_Wear_E004",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E006.SK_P008_Wear_E006"
        };
        public string[] hendrikBodyList = new string[]
        {
           "Default",
           "Hendrik's Armour",
           "Drustan's Armour",
           "General's Jacket",
           "Heliodor Knight Armour"
        };
        public string[] hendrikHeadArr = new string[]
        {
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E001.SK_P008_Hair001_E001",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E003.SK_P008_Hair001_E003",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E004.SK_P008_Hair001_E004"
        };
        public string[] hendrikHeadList = new string[]
        {
            "Default",
            "Drustan's Helm",
            "Short Hair [General's Jacket]"
        };
        //All (no NPC)
        public string[] allBodyArr = new string[]
        {
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E001.SK_P001_Wear_E001",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E002.SK_P001_Wear_E002",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E004.SK_P001_Wear_E004",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E005.SK_P001_Wear_E005",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E007.SK_P001_Wear_E007",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E008.SK_P001_Wear_E008",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E009.SK_P001_Wear_E009",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E010.SK_P001_Wear_E010",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E001.SK_P002_Wear_E001",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E002.SK_P002_Wear_E002",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E003.SK_P002_Wear_E003",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E004.SK_P002_Wear_E004",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E005.SK_P002_Wear_E005",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E007.SK_P002_Wear_E007",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E001.SK_P003_Wear_E001",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E002.SK_P003_Wear_E002",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E003.SK_P003_Wear_E003",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E004.SK_P003_Wear_E004",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E005.SK_P003_Wear_E005",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E006.SK_P003_Wear_E006",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E001.SK_P004_Wear_E001",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E002.SK_P004_Wear_E002",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E003.SK_P004_Wear_E003",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E004.SK_P004_Wear_E004",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E005.SK_P004_Wear_E005",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E006.SK_P004_Wear_E006",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E001.SK_P005_Wear_E001",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E003.SK_P005_Wear_E003",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E004.SK_P005_Wear_E004",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E001.SK_P006_Wear_E001",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E002.SK_P006_Wear_E002",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E003.SK_P006_Wear_E003",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E004.SK_P006_Wear_E004",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E005.SK_P006_Wear_E005",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E006.SK_P006_Wear_E006",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E007.SK_P006_Wear_E007",
            "/Game/Characters/Human/P007/Wear/Mesh/SK_P007_Wear_E001.SK_P007_Wear_E001",
            "/Game/Characters/Human/P007/Wear/Mesh/SK_P007_Wear_E002.SK_P007_Wear_E002",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E002.SK_P008_Wear_E002",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E001.SK_P008_Wear_E001",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E003.SK_P008_Wear_E003",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E004.SK_P008_Wear_E004",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E006.SK_P008_Wear_E006"
        };
        public string[] allBodyList = new string[]
        {
           "Luminary - Default",
           "Luminary - Cobblestone Clobber",
           "Luminary - Hot Spring Clothes",
           "Luminary - Gallopitan Garb",
           "Luminary - Erdwin's Coronet and Tunic",
           "Luminary - Mardi Garb",
           "Luminary - Drasilian Armour",
           "Luminary - Artisanal Trodain Togs",
           "Erik - Default",
           "Erik - Bald [Hood Up]",
           "Erik - Hot Spring Clothes",
           "Erik - Pirate King's Coat",
           "Erik - Swindler King's Stole",
           "Erik - Viking Clothes",
           "Veronica - Default",
           "Veronica - Gown of Eternity",
           "Veronica - Stellar Suit",
           "Veronica - Cat Suit",
           "Veronica - Pretty Pinny",
           "Veronica - Uniform de l'Academie",
           "Serena - Default",
           "Serena - Serenica's Surplice",
           "Serena - Sacred Raiment",
           "Serena - Dancer's Costume",
           "Serena - Dainty Dress",
           "Serena - Uniform de l'Academie",
           "Sylvando - Default",
           "Sylvando - Glad Rags",
           "Sylvando - Dapper Doublet",
           "Jade - Default",
           "Jade - Xenlon Gown",
           "Jade - Minerva's Raiment",
           "Jade - Divine Bustier",
           "Jade - Scandalous Swimsuit",
           "Jade - Uniform de l'Academie",
           "Jade - Bunny Suit",
           "Rab - Default",
           "Rab - Pallium Regale",
           "Hendrik - Default",
           "Hendrik - Hendrik's Armour",
           "Hendrik - Drustan's Armour",
           "Hendrik - General's Jacket",
           "Hendrik - Heliodor Knight Armour"
        };
        public string[] allHeadArr = new string[]
        {
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E001.SK_P001_Hair001_E001",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E003.SK_P001_Hair001_E003",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E007.SK_P001_Hair001_E007",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E009.SK_P001_Hair001_E009",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E010.SK_P001_Hair001_E010",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E011.SK_P001_Hair001_E011",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair001_E001.SK_N101_Hair001_E001",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E001.SK_P002_Hair001_E001",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E002.SK_P002_Hair001_E002",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E004.SK_P002_Hair001_E004",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E005.SK_P002_Hair001_E005",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E001.SK_P003_Hair001_E001",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E002.SK_P003_Hair001_E002",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E003.SK_P003_Hair001_E003",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E004.SK_P003_Hair001_E004",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E005.SK_P003_Hair001_E005",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E006.SK_P003_Hair001_E006",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E001.SK_P004_Hair001_E001",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E003.SK_P004_Hair001_E003",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E004.SK_P004_Hair001_E004",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E007.SK_P004_Hair001_E007",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E009.SK_P004_Hair001_E009",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E012.SK_P004_Hair001_E012",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E001.SK_P005_Hair001_E001",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E002.SK_P005_Hair001_E002",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E004.SK_P005_Hair001_E004",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E001.SK_P006_Hair001_E001",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E002.SK_P006_Hair001_E002",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E003.SK_P006_Hair001_E003",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E004.SK_P006_Hair001_E004",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E007.SK_P006_Hair001_E007",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E001.SK_P007_Hair001_E001",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E002.SK_P007_Hair001_E002",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E003.SK_P007_Hair001_E003",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E001.SK_P008_Hair001_E001",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E003.SK_P008_Hair001_E003",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E004.SK_P008_Hair001_E004"
        };
        public string[] allHeadList = new string[]
        {
            "Luminary - Default",
            "Luminary -",
            "Luminary - Erdwin's Coronet",
            "Luminary - Drasilian Helm",
            "Luminary - Artisanal Trodian Bandana",
            "Luminary - Octagonia Tournament Mask",
            "Luminary - Gallopolis Race Helmet (Use with Gallopolis Face Mesh!)",
            "Erik - Default",
            "Erik - Hood Up",
            "Erik - Pirate King's Cap",
            "Erik - Swindler's Scarf",
            "Veronica - Default",
            "Veronica - Crown of Eternity",
            "Veronica - Stellar Starflower",
            "Veronica - Cat Hat",
            "Veronica - Cute Cap",
            "Veronica - Uniform de l'Academie [Hat]",
            "Serena - Default [Long]",
            "Serena - Serenica's Circlet [Long]",
            "Serena - Dancer's Costume [Long]",
            "Serena - Default [Short]",
            "Serena - Dancer's Costume [Short]",
            "Serena - Sacred Circlet [Short]",
            "Sylvando - Default",
            "Sylvando - Mardi Garb",
            "Sylvando - Head-Turner",
            "Jade - Default",
            "Jade - Xenlon Hair Ring",
            "Jade - Minerva's Tiara",
            "Jade - Divine Bustier [Flower]",
            "Jade - Bunny Ears",
            "Rab - Default",
            "Rab - Sun Crown",
            "Rab - Yggdrasil Crown",
            "Hendrik - Default",
            "Hendrik - Drustan's Helm",
            "Hendrik - Short Hair [General's Jacket]"
        };
        public string[] faceMeshArr = new string[]
        {
            "/Game/Characters/Human/P001/Face/Mesh/SK_P001_Face001_E001.SK_P001_Face001_E001",
            "/Game/Characters/Human/P001/Face/Mesh/SK_P001_Face001_E006.SK_P001_Face001_E006",
            "/Game/Characters/Human/P002/Face/Mesh/SK_P002_Face001_E001.SK_P002_Face001_E001",
            "/Game/Characters/Human/P003/Face/Mesh/SK_P003_Face001_E001.SK_P003_Face001_E001",
            "/Game/Characters/Human/P004/Face/Mesh/SK_P004_Face001_E001.SK_P004_Face001_E001",
            "/Game/Characters/Human/P005/Face/Mesh/SK_P005_Face001_E001.SK_P005_Face001_E001",
            "/Game/Characters/Human/P006/Face/Mesh/SK_P006_Face001_E001.SK_P006_Face001_E001",
            "/Game/Characters/Human/P006/Face/Mesh/SK_P006_Face001_E007.SK_P006_Face001_E007",
            "/Game/Characters/Human/P007/Face/Mesh/SK_P007_Face001_E001.SK_P007_Face001_E001",
            "/Game/Characters/Human/P008/Face/Mesh/SK_P008_Face001_E001.SK_P008_Face001_E001"

        };
        public string[] faceMeshList = new string[]
        {
            "Luminary",
            "Gallopolis Race Mask (Luminary)",
            "Erik",
            "Veronica",
            "Serena",
            "Sylvando",
            "Jade",
            "Jinxed Jade",
            "Rab",
            "Hendrik"
        };
        public string[] baseMeshArr = new string[]
        {
            "/Game/Characters/Human/P001/Base/Mesh/SK_P001.SK_P001",
            "/Game/Characters/Human/P002/Base/Mesh/SK_P002.SK_P002",
            "/Game/Characters/Human/P003/Base/Mesh/SK_P003.SK_P003",
            "/Game/Characters/Human/P004/Base/Mesh/SK_P004.SK_P004",
            "/Game/Characters/Human/P005/Base/Mesh/SK_P005.SK_P005",
            "/Game/Characters/Human/P006/Base/Mesh/SK_P006.SK_P006",
            "/Game/Characters/Human/P007/Base/Mesh/SK_P007.SK_P007",
            "/Game/Characters/Human/P008/Base/Mesh/SK_P008.SK_P008"
        };
        public string[] baseMeshList = new string[]
        {
            "Luminary",
            "Erik",
            "Veronica",
            "Serena",
            "Sylvando",
            "Jade",
            "Rab",
            "Hendrik"
        };
        //All (NPC)
        public string[] npcAllBodyArr = new string[]
        {
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E001.SK_P001_Wear_E001",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E002.SK_P001_Wear_E002",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E004.SK_P001_Wear_E004",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E005.SK_P001_Wear_E005",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E007.SK_P001_Wear_E007",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E008.SK_P001_Wear_E008",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E009.SK_P001_Wear_E009",
            "/Game/Characters/Human/P001/Wear/Mesh/SK_P001_Wear_E010.SK_P001_Wear_E010",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E001.SK_P002_Wear_E001",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E002.SK_P002_Wear_E002",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E003.SK_P002_Wear_E003",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E004.SK_P002_Wear_E004",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E005.SK_P002_Wear_E005",
            "/Game/Characters/Human/P002/Wear/Mesh/SK_P002_Wear_E007.SK_P002_Wear_E007",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E001.SK_P003_Wear_E001",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E002.SK_P003_Wear_E002",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E003.SK_P003_Wear_E003",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E004.SK_P003_Wear_E004",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E005.SK_P003_Wear_E005",
            "/Game/Characters/Human/P003/Wear/Mesh/SK_P003_Wear_E006.SK_P003_Wear_E006",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E001.SK_P004_Wear_E001",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E002.SK_P004_Wear_E002",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E003.SK_P004_Wear_E003",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E004.SK_P004_Wear_E004",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E005.SK_P004_Wear_E005",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E006.SK_P004_Wear_E006",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E001.SK_P005_Wear_E001",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E003.SK_P005_Wear_E003",
            "/Game/Characters/Human/P005/Wear/Mesh/SK_P005_Wear_E004.SK_P005_Wear_E004",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E001.SK_P006_Wear_E001",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E002.SK_P006_Wear_E002",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E003.SK_P006_Wear_E003",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E004.SK_P006_Wear_E004",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E005.SK_P006_Wear_E005",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E006.SK_P006_Wear_E006",
            "/Game/Characters/Human/P006/Wear/Mesh/SK_P006_Wear_E007.SK_P006_Wear_E007",
            "/Game/Characters/Human/P007/Wear/Mesh/SK_P007_Wear_E001.SK_P007_Wear_E001",
            "/Game/Characters/Human/P007/Wear/Mesh/SK_P007_Wear_E002.SK_P007_Wear_E002",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E002.SK_P008_Wear_E002",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E001.SK_P008_Wear_E001",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E003.SK_P008_Wear_E003",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E004.SK_P008_Wear_E004",
            "/Game/Characters/Human/P008/Wear/Mesh/SK_P008_Wear_E006.SK_P008_Wear_E006",
            "/Game/Characters/Human/N101/Wear/Mesh/SK_N101_Wear_E210.SK_N101_Wear_E210",
            "/Game/Characters/Human/N002/Wear/Mesh/SK_N002_Wear_E203.SK_N002_Wear_E203",
            "/Game/Characters/Human/N101/Wear/Mesh/SK_N101_Wear_E201.SK_N101_Wear_E201",
            "/Game/Characters/Human/N001/Wear/Mesh/SK_N001_Wear_E205.SK_N001_Wear_E205",
            "/Game/Characters/Human/N001/Wear/Mesh/SK_N001_Wear_E020.SK_N001_Wear_E020",
            "/Game/Characters/Human/N101/Wear/Mesh/SK_N101_Wear_E204.SK_N101_Wear_E204",
            "/Game/Characters/Human/N001/Wear/Mesh/SK_N001_Wear_E006.SK_N001_Wear_E006",
            "/Game/Characters/Human/N108/Wear/Mesh/SK_N108_Wear_E206.SK_N108_Wear_E206",
            "/Game/Characters/Human/N108/Wear/Mesh/SK_N108_Wear_E204.SK_N108_Wear_E204",
            "/Game/Characters/Human/P004/Wear/Mesh/SK_P004_Wear_E004.SK_P004_Wear_E004",
            "/Game/Characters/Human/N002/Wear/Mesh/SK_N002_Wear_E015.SK_N002_Wear_E015",
            "/Game/Characters/Human/N001/Wear/Mesh/SK_N001_Wear_E015.SK_N001_Wear_E015",
            "/Game/Characters/Human/N001/Wear/Mesh/SK_N001_Wear_E012.SK_N001_Wear_E012",
            "/Game/Characters/Human/N101/Wear/Mesh/SK_N101_Wear_E002.SK_N101_Wear_E002",
            "/Game/Characters/Human/N102/Wear/Mesh/SK_N102_Wear_E202.SK_N102_Wear_E202",
            "/Game/Characters/Human/N101/Wear/Mesh/SK_N101_Wear_E207.SK_N101_Wear_E207",
            "/Game/Characters/Human/N002/Wear/Mesh/SK_N002_Wear_E010.SK_N002_Wear_E010",
            "/Game/Characters/Human/N001/Wear/Mesh/SK_N001_Wear_E006.SK_N001_Wear_E006"
        };
        public string[] npcAllBodyList = new string[]
        {
            "Luminary - Default",
            "Luminary - Cobblestone Clobber",
            "Luminary - Hot Spring Clothes",
            "Luminary - Gallopitan Garb",
            "Luminary - Erdwin's Coronet and Tunic",
            "Luminary - Mardi Garb",
            "Luminary - Drasilian Armour",
            "Luminary - Artisanal Trodain Togs",
            "Erik - Default",
            "Erik - Bald [Hood Up]",
            "Erik - Hot Spring Clothes",
            "Erik - Pirate King's Coat",
            "Erik - Swindler King's Stole",
            "Erik - Viking Clothes",
            "Veronica - Default",
            "Veronica - Gown of Eternity",
            "Veronica - Stellar Suit",
            "Veronica - Cat Suit",
            "Veronica - Pretty Pinny",
            "Veronica - Uniform de l'Academie",
            "Serena - Default",
            "Serena - Serenica's Surplice",
            "Serena - Sacred Raiment",
            "Serena - Dancer's Costume",
            "Serena - Dainty Dress",
            "Serena - Uniform de l'Academie",
            "Sylvando - Default",
            "Sylvando - Glad Rags",
            "Sylvando - Dapper Doublet",
            "Jade - Default",
            "Jade - Xenlon Gown",
            "Jade - Minerva's Raiment",
            "Jade - Divine Bustier",
            "Jade - Scandalous Swimsuit",
            "Jade - Uniform de l'Academie",
            "Jade - Bunny Suit",
            "Rab - Default",
            "Rab - Pallium Regale",
            "Hendrik - Default",
            "Hendrik - Hendrik's Armour",
            "Hendrik - Drustan's Armour",
            "Hendrik - General's Jacket",
            "Hendrik - Heliodor Knight Armour",
            "Erdwin",
            "Gemma",
            "King Irwin",
            "Morcant",
            "Beastly Boys Trainee",
            "Jasper",
            "Marcello",
            "Don Rodrigo",
            "Vince Vanquish",
            "Maya",
            "Nera",
            "Madason",
            "Kiefer",
            "Mervyn",
            "Terry",
            "Rek",
            "Alef",
            "Prince Midenhall"
        };
        public string[] npcAllHeadArr = new string[]
        {
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E001.SK_P001_Hair001_E001",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E003.SK_P001_Hair001_E003",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E007.SK_P001_Hair001_E007",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E009.SK_P001_Hair001_E009",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E010.SK_P001_Hair001_E010",
            "/Game/Characters/Human/P001/Hair/Mesh/SK_P001_Hair001_E011.SK_P001_Hair001_E011",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair001_E001.SK_N101_Hair001_E001",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E001.SK_P002_Hair001_E001",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E002.SK_P002_Hair001_E002",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E004.SK_P002_Hair001_E004",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E005.SK_P002_Hair001_E005",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E001.SK_P003_Hair001_E001",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E002.SK_P003_Hair001_E002",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E003.SK_P003_Hair001_E003",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E004.SK_P003_Hair001_E004",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E005.SK_P003_Hair001_E005",
            "/Game/Characters/Human/P003/Hair/Mesh/SK_P003_Hair001_E006.SK_P003_Hair001_E006",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E001.SK_P004_Hair001_E001",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E003.SK_P004_Hair001_E003",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E004.SK_P004_Hair001_E004",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E007.SK_P004_Hair001_E007",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E009.SK_P004_Hair001_E009",
            "/Game/Characters/Human/P004/Hair/Mesh/SK_P004_Hair001_E012.SK_P004_Hair001_E012",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E001.SK_P005_Hair001_E001",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E002.SK_P005_Hair001_E002",
            "/Game/Characters/Human/P005/Hair/Mesh/SK_P005_Hair001_E004.SK_P005_Hair001_E004",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E001.SK_P006_Hair001_E001",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E002.SK_P006_Hair001_E002",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E003.SK_P006_Hair001_E003",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E004.SK_P006_Hair001_E004",
            "/Game/Characters/Human/P006/Hair/Mesh/SK_P006_Hair001_E007.SK_P006_Hair001_E007",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E001.SK_P007_Hair001_E001",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E002.SK_P007_Hair001_E002",
            "/Game/Characters/Human/P007/Hair/Mesh/SK_P007_Hair001_E003.SK_P007_Hair001_E003",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E001.SK_P008_Hair001_E001",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E003.SK_P008_Hair001_E003",
            "/Game/Characters/Human/P008/Hair/Mesh/SK_P008_Hair001_E004.SK_P008_Hair001_E004",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair001_E210.SK_N101_Hair001_E210",
            "/Game/Characters/Human/N002/Hair/Mesh/SK_N002_Hair005_E203.SK_N002_Hair005_E203",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair003_E201.SK_N101_Hair003_E201",
            "/Game/Characters/Human/N001/Hair/Mesh/SK_N001_Hair003_E205.SK_N001_Hair003_E205",
            "/Game/Characters/Human/N001/Hair/Mesh/SK_N001_Hair003_E005.SK_N001_Hair003_E005",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair004_E204.SK_N101_Hair004_E204",
            "/Game/Characters/Human/N001/Hair/Mesh/SK_N001_Hair001_E004.SK_N001_Hair001_E004",
            "/Game/Characters/Human/N108/Hair/Mesh/SK_N108_Hair006_E207.SK_N108_Hair006_E207",
            "/Game/Characters/Human/N108/Hair/Mesh/SK_N108_Hair005_E204.SK_N108_Hair005_E204",
            "/Game/Characters/Human/N104/Hair/Mesh/SK_N104_Hair001_E201.SK_N104_Hair001_E201",
            "/Game/Characters/Human/N002/Hair/Mesh/SK_N002_Hair002_E216.SK_N002_Hair002_E216",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E005.SK_P002_Hair001_E005",
            "/Game/Characters/Human/N002/Hair/Mesh/SK_N002_Hair005_E204.SK_N002_Hair005_E204",
            "/Game/Characters/Human/N011/Hair/Mesh/SK_N011_Hair001_E106.SK_N011_Hair001_E106",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair004_E206.SK_N101_Hair004_E206",
            "/Game/Characters/Human/P002/Hair/Mesh/SK_P002_Hair001_E001.SK_P002_Hair001_E001",
            "/Game/Characters/Human/N101/Hair/Mesh/SK_N101_Hair003_E203.SK_N101_Hair003_E203",
            "/Game/Characters/Human/N003/Hair/Mesh/SK_N003_Hair001_E105.SK_N003_Hair001_E105"
        };
        public string[] npcAllHeadList = new string[]
        {
            "Luminary - Default",
            "Luminary -",
            "Luminary - Erdwin's Coronet",
            "Luminary - Drasilian Helm",
            "Luminary - Artisanal Trodian Bandana",
            "Luminary - Octagonia Tournament Mask",
            "Luminary - Gallopolis Race Helmet (Use with Gallopolis Face Mesh!)",
            "Erik - Default",
            "Erik - Hood Up",
            "Erik - Pirate King's Cap",
            "Erik - Swindler's Scarf",
            "Veronica - Default",
            "Veronica - Crown of Eternity",
            "Veronica - Stellar Starflower",
            "Veronica - Cat Hat",
            "Veronica - Cute Cap",
            "Veronica - Uniform de l'Academie [Hat]",
            "Serena - Default [Long]",
            "Serena - Serenica's Circlet [Long]",
            "Serena - Dancer's Costume [Long]",
            "Serena - Default [Short]",
            "Serena - Dancer's Costume [Short]",
            "Serena - Sacred Circlet [Short]",
            "Sylvando - Default",
            "Sylvando - Mardi Garb",
            "Sylvando - Head-Turner",
            "Jade - Default",
            "Jade - Xenlon Hair Ring",
            "Jade - Minerva's Tiara",
            "Jade - Divine Bustier [Flower]",
            "Jade - Bunny Ears",
            "Rab - Default",
            "Rab - Sun Crown",
            "Rab - Yggdrasil Crown",
            "Hendrik - Default",
            "Hendrik - Drustan's Helm",
            "Hendrik - Short Hair [General's Jacket]",
            "Erdwin",
            "Gemma",
            "King Irwin",
            "Morcant",
            "Beastly Boys Trainee",
            "Jasper",
            "Marcello",
            "Don Rodrigo",
            "Vince Vanquish",
            "Maya",
            "Nera",
            "Madason",
            "Kiefer",
            "Mervyn",
            "Terry",
            "Rek",
            "Alef",
            "Prince Midenhall"
        };
        public string[] npcFaceMeshArr = new string[]
        {
            "/Game/Characters/Human/P001/Face/Mesh/SK_P001_Face001_E001.SK_P001_Face001_E001",
            "/Game/Characters/Human/P001/Face/Mesh/SK_P001_Face001_E006.SK_P001_Face001_E006",
            "/Game/Characters/Human/P002/Face/Mesh/SK_P002_Face001_E001.SK_P002_Face001_E001",
            "/Game/Characters/Human/P003/Face/Mesh/SK_P003_Face001_E001.SK_P003_Face001_E001",
            "/Game/Characters/Human/P004/Face/Mesh/SK_P004_Face001_E001.SK_P004_Face001_E001",
            "/Game/Characters/Human/P005/Face/Mesh/SK_P005_Face001_E001.SK_P005_Face001_E001",
            "/Game/Characters/Human/P006/Face/Mesh/SK_P006_Face001_E001.SK_P006_Face001_E001",
            "/Game/Characters/Human/P006/Face/Mesh/SK_P006_Face001_E007.SK_P006_Face001_E007",
            "/Game/Characters/Human/P007/Face/Mesh/SK_P007_Face001_E001.SK_P007_Face001_E001",
            "/Game/Characters/Human/P008/Face/Mesh/SK_P008_Face001_E001.SK_P008_Face001_E001",
            "/Game/Characters/Human/N101/Face/Mesh/SK_N101_Face001_E210.SK_N101_Face001_E210",
            "/Game/Characters/Human/N002/Face/Mesh/SK_N002_Face005_E203.SK_N002_Face005_E203",
            "/Game/Characters/Human/N101/Face/Mesh/SK_N101_Face003_E201.SK_N101_Face003_E201",
            "/Game/Characters/Human/N001/Face/Mesh/SK_N001_Face003_E205.SK_N001_Face003_E205",
            "/Game/Characters/Human/N001/Face/Mesh/SK_N001_Face002_E003.SK_N001_Face002_E003",
            "/Game/Characters/Human/N101/Face/Mesh/SK_N101_Face004_E204.SK_N101_Face004_E204",
            "/Game/Characters/Human/N001/Face/Mesh/SK_N001_Face002_E002.SK_N001_Face002_E002",
            "/Game/Characters/Human/N108/Face/Mesh/SK_N108_Face006_E206.SK_N108_Face006_E206",
            "/Game/Characters/Human/N108/Face/Mesh/SK_N108_Face005_E204.SK_N108_Face005_E204",
            "/Game/Characters/Human/P006/Face/Mesh/SK_P006_Face001_E001.SK_P006_Face001_E001",
            "/Game/Characters/Human/P004/Face/Mesh/SK_P004_Face001_E001.SK_P004_Face001_E001",
            "/Game/Characters/Human/P001/Face/Mesh/SK_P001_Face001_E001.SK_P001_Face001_E001",
            "/Game/Characters/Human/P002/Face/Mesh/SK_P002_Face001_E001.SK_P002_Face001_E001",
            "/Game/Characters/Human/N108/Face/Mesh/SK_N108_Face006_E206.SK_N108_Face006_E206",
            "/Game/Characters/Human/P002/Face/Mesh/SK_P002_Face001_E001.SK_P002_Face001_E001",
            "/Game/Characters/Human/P002/Face/Mesh/SK_P002_Face001_E001.SK_P002_Face001_E001",
            "/Game/Characters/Human/N101/Face/Mesh/SK_N101_Face001_E210.SK_N101_Face001_E210",
            "/Game/Characters/Human/N002/Face/Mesh/SK_N002_Face001_E217.SK_N002_Face001_E217",
            "/Game/Characters/Human/N101/Face/Mesh/SK_N101_Face003_E201.SK_N101_Face003_E201"
        };
        public string[] npcFaceMeshList = new string[]
        {
            "Luminary",
            "Gallopolis Race Mask (Luminary)",
            "Erik",
            "Veronica",
            "Serena",
            "Sylvando",
            "Jade",
            "Jinxed Jade",
            "Rab",
            "Hendrik",
            "Erdwin",
            "Gemma",
            "King Irwin",
            "Morcant",
            "Beastly Boys Trainee",
            "Jasper",
            "Marcello",
            "Don Rodrigo",
            "Vince Vanquish",
            "Maya",
            "Nera",
            "Madason",
            "Kiefer",
            "Mervyn",
            "Terry",
            "Rek",
            "Alef",
            "Prince Midenhall",
            "Irwin (Dundrasil Knight)"
        };
        public string[] npcBaseMeshArr = new string[]
        {
            "/Game/Characters/Human/P001/Base/Mesh/SK_P001.SK_P001",
            "/Game/Characters/Human/P002/Base/Mesh/SK_P002.SK_P002",
            "/Game/Characters/Human/P003/Base/Mesh/SK_P003.SK_P003",
            "/Game/Characters/Human/P004/Base/Mesh/SK_P004.SK_P004",
            "/Game/Characters/Human/P005/Base/Mesh/SK_P005.SK_P005",
            "/Game/Characters/Human/P006/Base/Mesh/SK_P006.SK_P006",
            "/Game/Characters/Human/P007/Base/Mesh/SK_P007.SK_P007",
            "/Game/Characters/Human/P008/Base/Mesh/SK_P008.SK_P008",
            "/Game/Characters/Human/PXXX/Base/Mesh/SK_PXXX.SK_PXXX",
            "/Game/Characters/Human/N101/Base/Mesh/SK_N101.SK_N101",
            "/Game/Characters/Human/N002/Base/Mesh/SK_N002.SK_N002",
            "/Game/Characters/Human/N101/Base/Mesh/SK_N101.SK_N101",
            "/Game/Characters/Human/N001/Base/Mesh/SK_N001.SK_N001",
            "/Game/Characters/Human/N001/Base/Mesh/SK_N001.SK_N001",
            "/Game/Characters/Human/N101/Base/Mesh/SK_N101.SK_N101",
            "/Game/Characters/Human/N001/Base/Mesh/SK_N001.SK_N001",
            "/Game/Characters/Human/N108/Base/Mesh/SK_N108.SK_N108",
            "/Game/Characters/Human/N108/Base/Mesh/SK_N108.SK_N108",
            "/Game/Characters/Human/P004/Base/Mesh/SK_P004.SK_P004",
            "/Game/Characters/Human/N002/Base/Mesh/SK_N002.SK_N002",
            "/Game/Characters/Human/P001/Base/Mesh/SK_P001.SK_P001",
            "/Game/Characters/Human/N002/Base/Mesh/SK_N002.SK_N002",
            "/Game/Characters/Human/P001/Base/Mesh/SK_P001.SK_P001",
            "/Game/Characters/Human/N106/Base/Mesh/SK_N106.SK_N106",
            "/Game/Characters/Human/P002/Base/Mesh/SK_P002.SK_P002",
            "/Game/Characters/Human/N002/Base/Mesh/SK_N002.SK_N002",
            "/Game/Characters/Human/N106/Base/Mesh/SK_N106.SK_N106",
            "/Game/Characters/Human/N101/Base/Mesh/SK_N101.SK_N101"
        };
        public string[] npcBaseMeshList = new string[]
        {
            "Luminary",
            "Erik",
            "Veronica",
            "Serena",
            "Sylvando",
            "Jade",
            "Rab",
            "Hendrik",
            "Universal Base Mesh",
            "Erdwin",
            "Gemma",
            "King Irwin",
            "Morcant",
            "Beastly Boys",
            "Jasper",
            "Marcello",
            "Don Rodrigo",
            "Vince Vanquish",
            "Maya",
            "Nera",
            "Madason",
            "Kiefer",
            "Mervyn",
            "Terry",
            "Rek",
            "Alef",
            "Prince Midenhall",
            "Irwin (Dundrasil Knight)"
        };

    }
}


