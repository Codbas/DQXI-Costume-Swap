using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DQXICostumeSwap.Utilities
{
    public class GetName
    {
        public string[] comboboxes = new string[32]
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
        public string[] comboboxesBasic = new string[16]
        {
           "luminaryBodyComboBox",
           "luminaryHeadComboBox",
           "erikBodyComboBox",
           "erikHeadComboBox",
           "veronicaBodyComboBox",
           "veronicaHeadComboBox",
           "serenaBodyComboBox",
           "serenaHeadComboBox",
           "sylvandoBodyComboBox",
           "sylvandoHeadComboBox",
           "jadeBodyComboBox",
           "jadeHeadComboBox",
           "rabBodyComboBox",
           "rabHeadComboBox",
           "hendrikBodyComboBox",
           "hendrikHeadComboBox",
        };
        public string[] names = new string[32]
        {
            "Luminary Body",
            "Luminary Head",
            "Luminary Face",
            "Luminary Base",
            "Erik Body",
            "Erik Head",
            "Erik Face",
            "Erik Base",
            "Veronica Body",
            "Veronica Head",
            "Veronica Face",
            "Veronica Base",
            "Serena Body",
            "Serena Head",
            "Serena Face",
            "Serena Base",
            "Sylvando Body",
            "Sylvando Head",
            "Sylvando Face",
            "Sylvando Base",
            "Jade Body",
            "Jade Head",
            "Jade Face",
            "Jade Base",
            "Rab Body",
            "Rab Head",
            "Rab Face",
            "Rab Base",
            "Hendrik Body",
            "Hendrik Head",
            "Hendrik Face",
            "Hendrik Base",
        };
        public string[] namesBasic = new string[16]
        {
            "Luminary Body",
            "Luminary Head",
            "Erik Body",
            "Erik Head",
            "Veronica Body",
            "Veronica Head",
            "Serena Body",
            "Serena Head",
            "Sylvando Body",
            "Sylvando Head",
            "Jade Body",
            "Jade Head",
            "Rab Body",
            "Rab Head",
            "Hendrik Body",
            "Hendrik Head",
        };


        public string NamesToCB(string s, int a)
        {
            string name = "";
            int i = 0;
            while (i < a)
            {
                switch (a)
                {
                    case 16:
                        {
                            if (String.Equals(namesBasic[i], s))
                            {
                                name = comboboxesBasic[i];
                                i = a;
                            }
                            i++;
                            break;
                        }
                    case 32:
                        {
                            if (String.Equals(names[i], s))
                            {
                                name = comboboxes[i];
                                i = a;
                            }
                            i++;
                            break;
                        }
                    default:
                        {
                            i = a;
                            Console.WriteLine($"Error: NamesToCB int a value invalid. a = {a}");
                            MessageBox.Show("Error saving data");
                            break;
                        }
                }
            }
            return name;
        }

        public string CBtoNames(string s, int a)
        {
            string name = "";
            int i = 0; 
            while (i < a)
            {
                switch (a)
                {
                    case 16:
                        {
                            if (String.Equals(comboboxesBasic[i], s))
                            {
                                name = namesBasic[i];
                                i = a;
                            }
                            i++;
                            break;
                        }
                    case 32:
                        {
                            if (String.Equals(comboboxes[i], s))
                            {
                                name = names[i];
                                i = a;
                            }
                            i++;
                            break;
                        }
                    default:
                        {
                            i = a;
                            Console.WriteLine($"Error: NamesToCB int a value invalid. a = {a}");
                            MessageBox.Show("Error saving data");
                            break;
                        }
                }
            }
            return name;
        }



    }
}
