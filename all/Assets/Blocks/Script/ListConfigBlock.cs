using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class ListConfigBlock
{
	public enum TypeBlock
	{
		mega_3_section,
		mega_activator_active,
		mega_activator_pasive,
		mega_bonus_black,
		mega_bonus_wite,
		mega_exploid_black,
		mega_exploid_white,
		mega_full_armor,
		mega_half_armor,
		mega_simple,
		mega_up,
		mega_very_simple,
		mega_very_simple_half,
		mega_very_simple_half_4
	}
	
	public TypeBlock ConfigSelection;

}