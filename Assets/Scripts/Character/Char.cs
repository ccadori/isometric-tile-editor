using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Char {
	
	[XmlElement]
	public int faseLevel;
	[XmlElement]
	public string charName;
	[XmlElement]
	public int level;
	[XmlElement]
	public int head;
	[XmlElement]
	public int weapon1;
	[XmlElement]
	public int weapon2;
	[XmlElement]
	public int robe;
}
