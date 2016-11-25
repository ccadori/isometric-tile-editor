using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Tile {
	
	[XmlAttribute]
	public int type;
	[XmlAttribute]
	public int x;
	[XmlAttribute]
	public int y;
	[XmlAttribute]
	public int z;
	[XmlAttribute]
	public int start;
}