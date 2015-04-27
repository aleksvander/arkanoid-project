using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class ListBonus
{
	public enum TypeBonus
	{
		none,
		bomb,
		life,
		multipli5,
		multipli3,
		gun,
		smallBall,
		normalBall,
		speedUp,
		speedDown,
		bigShield,
		smallSheild,
		fireball
	}
	
	public TypeBonus ConfigSelection;
	
}