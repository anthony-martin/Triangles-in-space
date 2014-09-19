using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Primitives;
using TrianglesInSpace.Objects;
using TrianglesInSpace.Motion;

namespace TrianglesInSpace.Vessels
{
    public interface IVessel
    {
        string Name{get;}
        Vector GetPosition(ulong time);
        Angle GetHeading(ulong time);
        List<IWeaponSystem> AvailableWeapons(Facing toTarget, double distance);
    }

    public class Vessel : IVessel
    {
        private int m_HitPoints = 1;
        //private int m_Armour;
        
        private List<IWeaponSystem> m_Weapons;
        private readonly SelectableObject m_Position;


        public Vessel(SelectableObject position)
        {
            m_Position = position;
            m_Weapons = new List<IWeaponSystem>
                {
                    new LanceBattery(),
                };
        }

        public string Name
        {
            get
            {
                return m_Position.Name;
            }
        }

        public Vector GetPosition(ulong time)
        {
            return m_Position.Path.GetCurrentMotion(time).GetCurrentPosition(time);
        }

        public Angle GetHeading(ulong time)
        {
            return new Angle(m_Position.Path.GetCurrentMotion(time).GetVelocity(time));
        }

        public List<IWeaponSystem> AvailableWeapons(Facing toTarget, double distance)
        {
            List<IWeaponSystem> available = new List<IWeaponSystem>();

            foreach(var weapon in m_Weapons)
            {
                //todo check if weapon has been fired recently here
                if(weapon.FireArc.HasFlag( toTarget) && weapon.Range > distance)
                {
                    available.Add(weapon);
                }
            }
            return available;
        }
    }
}
