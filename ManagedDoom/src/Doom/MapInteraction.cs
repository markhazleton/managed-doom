﻿using System;

namespace ManagedDoom
{
    public sealed class MapInteraction
    {
        private World world;

        public MapInteraction(World world)
        {
            this.world = world;
        }

		//
		// P_UseSpecialLine
		// Called when a thing uses a special line.
		// Only the front sides of lines are usable.
		//
		public bool UseSpecialLine(Mobj thing, LineDef line, int side)
		{
			var sa = world.SectorAction;

			// Err...
			// Use the back sides of VERY SPECIAL lines...
			if (side != 0)
			{
				switch ((int)line.Special)
				{
					case 124:
						// Sliding door open&close
						// UNUSED?
						break;

					default:
						return false;
				}
			}

			// Switches that other things can activate.
			if (thing.Player == null)
			{
				// never open secret doors
				if ((line.Flags & LineFlags.Secret) != 0)
				{
					return false;
				}

				switch ((int)line.Special)
				{
					case 1: // MANUAL DOOR RAISE
					case 32: // MANUAL BLUE
					case 33: // MANUAL RED
					case 34: // MANUAL YELLOW
						break;

					default:
						return false;
				}
			}

			// do something  
			switch ((int)line.Special)
			{
				// MANUALS
				case 1: // Vertical Door
				case 26: // Blue Door/Locked
				case 27: // Yellow Door /Locked
				case 28: // Red Door /Locked

				case 31: // Manual door open
				case 32: // Blue locked door open
				case 33: // Red locked door open
				case 34: // Yellow locked door open

				case 117: // Blazing door raise
				case 118: // Blazing door open
					sa.EV_VerticalDoor(line, thing);
					break;

					//UNUSED - Door Slide Open&Close
					// case 124:
					// EV_SlidingDoor (line, thing);
					// break;

					/*
					// SWITCHES
					case 7:
						// Build Stairs
						if (EV_BuildStairs(line, build8))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 9:
						// Change Donut
						if (EV_DoDonut(line))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 11:
						// Exit level
						P_ChangeSwitchTexture(line, 0);
						G_ExitLevel();
						break;

					case 14:
						// Raise Floor 32 and change texture
						if (EV_DoPlat(line, raiseAndChange, 32))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 15:
						// Raise Floor 24 and change texture
						if (EV_DoPlat(line, raiseAndChange, 24))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 18:
						// Raise Floor to next highest floor
						if (EV_DoFloor(line, raiseFloorToNearest))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 20:
						// Raise Plat next highest floor and change texture
						if (EV_DoPlat(line, raiseToNearestAndChange, 0))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 21:
						// PlatDownWaitUpStay
						if (EV_DoPlat(line, downWaitUpStay, 0))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 23:
						// Lower Floor to Lowest
						if (EV_DoFloor(line, lowerFloorToLowest))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 29:
						// Raise Door
						if (EV_DoDoor(line, normal))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 41:
						// Lower Ceiling to Floor
						if (EV_DoCeiling(line, lowerToFloor))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 71:
						// Turbo Lower Floor
						if (EV_DoFloor(line, turboLower))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 49:
						// Ceiling Crush And Raise
						if (EV_DoCeiling(line, crushAndRaise))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 50:
						// Close Door
						if (EV_DoDoor(line, close))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 51:
						// Secret EXIT
						P_ChangeSwitchTexture(line, 0);
						G_SecretExitLevel();
						break;

					case 55:
						// Raise Floor Crush
						if (EV_DoFloor(line, raiseFloorCrush))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 101:
						// Raise Floor
						if (EV_DoFloor(line, raiseFloor))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 102:
						// Lower Floor to Surrounding floor height
						if (EV_DoFloor(line, lowerFloor))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 103:
						// Open Door
						if (EV_DoDoor(line, open))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 111:
						// Blazing Door Raise (faster than TURBO!)
						if (EV_DoDoor(line, blazeRaise))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 112:
						// Blazing Door Open (faster than TURBO!)
						if (EV_DoDoor(line, blazeOpen))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 113:
						// Blazing Door Close (faster than TURBO!)
						if (EV_DoDoor(line, blazeClose))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 122:
						// Blazing PlatDownWaitUpStay
						if (EV_DoPlat(line, blazeDWUS, 0))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 127:
						// Build Stairs Turbo 16
						if (EV_BuildStairs(line, turbo16))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 131:
						// Raise Floor Turbo
						if (EV_DoFloor(line, raiseFloorTurbo))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 133:
					// BlzOpenDoor BLUE
					case 135:
					// BlzOpenDoor RED
					case 137:
						// BlzOpenDoor YELLOW
						if (EV_DoLockedDoor(line, blazeOpen, thing))
							P_ChangeSwitchTexture(line, 0);
						break;

					case 140:
						// Raise Floor 512
						if (EV_DoFloor(line, raiseFloor512))
							P_ChangeSwitchTexture(line, 0);
						break;

					// BUTTONS
					case 42:
						// Close Door
						if (EV_DoDoor(line, close))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 43:
						// Lower Ceiling to Floor
						if (EV_DoCeiling(line, lowerToFloor))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 45:
						// Lower Floor to Surrounding floor height
						if (EV_DoFloor(line, lowerFloor))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 60:
						// Lower Floor to Lowest
						if (EV_DoFloor(line, lowerFloorToLowest))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 61:
						// Open Door
						if (EV_DoDoor(line, open))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 62:
						// PlatDownWaitUpStay
						if (EV_DoPlat(line, downWaitUpStay, 1))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 63:
						// Raise Door
						if (EV_DoDoor(line, normal))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 64:
						// Raise Floor to ceiling
						if (EV_DoFloor(line, raiseFloor))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 66:
						// Raise Floor 24 and change texture
						if (EV_DoPlat(line, raiseAndChange, 24))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 67:
						// Raise Floor 32 and change texture
						if (EV_DoPlat(line, raiseAndChange, 32))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 65:
						// Raise Floor Crush
						if (EV_DoFloor(line, raiseFloorCrush))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 68:
						// Raise Plat to next highest floor and change texture
						if (EV_DoPlat(line, raiseToNearestAndChange, 0))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 69:
						// Raise Floor to next highest floor
						if (EV_DoFloor(line, raiseFloorToNearest))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 70:
						// Turbo Lower Floor
						if (EV_DoFloor(line, turboLower))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 114:
						// Blazing Door Raise (faster than TURBO!)
						if (EV_DoDoor(line, blazeRaise))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 115:
						// Blazing Door Open (faster than TURBO!)
						if (EV_DoDoor(line, blazeOpen))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 116:
						// Blazing Door Close (faster than TURBO!)
						if (EV_DoDoor(line, blazeClose))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 123:
						// Blazing PlatDownWaitUpStay
						if (EV_DoPlat(line, blazeDWUS, 0))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 132:
						// Raise Floor Turbo
						if (EV_DoFloor(line, raiseFloorTurbo))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 99:
					// BlzOpenDoor BLUE
					case 134:
					// BlzOpenDoor RED
					case 136:
						// BlzOpenDoor YELLOW
						if (EV_DoLockedDoor(line, blazeOpen, thing))
							P_ChangeSwitchTexture(line, 1);
						break;

					case 138:
						// Light Turn On
						EV_LightTurnOn(line, 255);
						P_ChangeSwitchTexture(line, 1);
						break;

					case 139:
						// Light Turn Off
						EV_LightTurnOn(line, 35);
						P_ChangeSwitchTexture(line, 1);
						break;
					*/
			}

			return true;
		}






		//
		// USE LINES
		//
		Mobj usething;

		private bool PTR_UseTraverse(Intercept ic)
		{
			var mc = world.MapCollision;

			if (ic.Line.Special == 0)
			{
				mc.LineOpening(ic.Line);
				if (mc.openRange <= Fixed.Zero)
				{
					world.StartSound(usething, Sfx.NOWAY);

					// can't use through a wall
					return false;
				}

				// not a special line, but keep checking
				return true;
			}

			var side = 0;
			if (Geometry.PointOnLineSide(usething.X, usething.Y, ic.Line) == 1)
			{
				side = 1;
			}

			// don't use back side
			//return false;

			UseSpecialLine(usething, ic.Line, side);

			// can't use for than one special line in a row
			return false;
		}

		//
		// P_UseLines
		// Looks for special lines in front of the player to activate.
		//
		public void UseLines(Player player)
		{
			var pt = world.PathTraversal;

			usething = player.Mobj;

			var angle = player.Mobj.Angle; // >> ANGLETOFINESHIFT;

			var x1 = player.Mobj.X;
			var y1 = player.Mobj.Y;
			var x2 = x1 + (World.USERANGE.Data >> Fixed.FracBits) * Trig.Cos(angle); // finecosine[angle];
			var y2 = y1 + (World.USERANGE.Data >> Fixed.FracBits) * Trig.Sin(angle); // finesine[angle];

			pt.PathTraverse(x1, y1, x2, y2, PathTraverseFlags.AddLines, ic => PTR_UseTraverse(ic));
		}
	}
}