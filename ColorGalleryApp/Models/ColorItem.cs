using System;
using System.Collections.Generic;
using System.Text;

namespace ColorGalleryApp.Models;

// Represent a color item with name and hex value
public class ColorItem
{
    public string? Name { get; set; }
    public string? Hex { get; set; }
    public Color ColorValue => Color.FromArgb(Hex);

    public ColorItem(string name, string hex)
    {
        Name = name;
        Hex = hex;
    }
}