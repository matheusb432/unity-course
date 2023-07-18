using UnityEngine;

namespace RPG.Example
{
    public sealed class Robot : MonoBehaviour
    {
        private IBatteryRegulations IncludedBattery { get; set; }

        public Robot()
        {
            IncludedBattery = new Battery(newHealth: 80);
            IncludedBattery.CheckHealth();
            IncludedBattery.CheckHealth();
            Charger.ChargeBattery(IncludedBattery);
            IncludedBattery.CheckHealth();
            print(Charger.chargerInUse);
        }
    }

    public class Battery : IBatteryRegulations
    {
        public float Health { get; set; }

        public Battery(float newHealth)
        {
            Health = newHealth;
            Debug.Log("New battery created!");
        }

        public void CheckHealth()
        {
            Debug.Log(Health);
        }

        public void SetHealth(float newHealth)
        {
            Health = newHealth;
        }
    }

    internal static class Charger
    {
        public static bool chargerInUse = false;

        public static void ChargeBattery(IBatteryRegulations battery)
        {
            chargerInUse = true;
            battery.SetHealth(100);
        }
    }

    public abstract class BatteryRegulations
    {
        public float Health { get; set; }

        public BatteryRegulations(float newHealth)
        {
            Health = newHealth;
        }

        public abstract void CheckHealth();
    }

    // ? Implementing BatteryRegulations with composition
    public interface IBatteryRegulations
    {
        void CheckHealth();

        void SetHealth(float newHealth);
    }
}
