using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LICStatsSystem
{
    public partial class Form1 : Form
    {
        string[] slots = new string[8];

        public Form1()
        {
            InitializeComponent();
        }

        public void SelectedItem(object sender, EventArgs e)
        {
            DoRPSLS();
        }

        private void DoRPSLS()
        {
            slots[0] = (string)comboBox1.SelectedItem;
            slots[1] = (string)comboBox2.SelectedItem;
            slots[2] = (string)comboBox3.SelectedItem;
            slots[3] = (string)comboBox4.SelectedItem;
            slots[4] = (string)comboBox5.SelectedItem;
            slots[5] = (string)comboBox6.SelectedItem;
            slots[6] = (string)comboBox7.SelectedItem;
            slots[7] = (string)comboBox8.SelectedItem;

            int weaponsMod = 0;
            int powerupsMod = 0;

            int strength = 0;
            int agility = 0;
            int speed = 0;
            int spirit = 0;
            int karma = 0;

            foreach (string slot in slots)
            {
                switch (slot)
                {
                    case "Strength":
                        strength++;
                        break;
                    case "Agility":
                        agility++;
                        break;
                    case "Speed":
                        speed++;
                        break;
                    case "Spirit":
                        spirit++;
                        break;
                    case "Karma":
                        karma++;
                        break;
                }
            }
            int strengthMod = strength;
            int agilityMod = agility;
            int speedMod = speed;
            int spiritMod = spirit;
            int karmaMod = karma;

            if (strength > 0)
            {
                if (spirit > 0)
                    spiritMod = spirit + strength;
                if (speed > 0)
                    speedMod = speed + strength;
                if (karma > 0)
                    karmaMod = karma - strength;
                if (agility > 0)
                    agilityMod = agility - strength;
                weaponsMod++;
            }
            if (agility > 0)
            {
                if (karma > 0)
                    karmaMod = karma + agility;
                if (speed > 0)
                    speedMod = speed + agility;
                if (strength > 0)
                    strengthMod = strength - agility;
                if (spirit > 0)
                    spiritMod = spirit - agility;
                weaponsMod++;
            }
            if (speed > 0)
            {
                if (strength > 0)
                    strengthMod = strength + speed;
                if (agility > 0)
                    agilityMod = agility + speed;
                if (karma > 0)
                    karmaMod = karma - speed;
                if (spirit > 0)
                    spiritMod = spirit - speed;
                powerupsMod++;
            }
            if (spirit > 0)
            {
                if (strength > 0)
                    strengthMod = strength + spirit;
                if (karma > 0)
                    karmaMod = karma + spirit;
                if (agility > 0)
                    agilityMod = agility - spirit;
                if (speed > 0)
                    speedMod = speed - spirit;
                powerupsMod++;
            }
            if (karma > 0)
            {
                if (spirit > 0)
                    spiritMod = spirit + karma;
                if (agility > 0)
                    agilityMod = agility + karma;
                if (strength > 0)
                    strengthMod = strength - karma;
                if (speed > 0)
                    speedMod = speed - karma;
                powerupsMod++;
                weaponsMod++;
            }

            Strength.Text = "Strength:   +" + strengthMod;
            Agility.Text = "Agility:   +" + agilityMod;
            Speed.Text = "Speed:   +" + speedMod;
            Spirit.Text = "Spirit:   +" + spiritMod;
            Karma.Text = "Karma:   +" + karmaMod;
            Weapons.Text = "Weapons:   +" + weaponsMod;
            Powerups.Text = "Powerups:   +" + powerupsMod;
        }
    }
}
