using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Alignment
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string alignment = "";

	}
}
