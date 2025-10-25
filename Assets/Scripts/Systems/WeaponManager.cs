using UnityEngine;
using System.Collections.Generic;

namespace DystopiaGirls.Systems
{
    /// <summary>
    /// 플레이어의 무기를 관리하는 시스템
    /// </summary>
    public class WeaponManager : MonoBehaviour
    {
        [Header("Weapon Container")]
        [SerializeField] private Transform weaponContainer;

        [Header("Available Weapons")]
        [SerializeField] private GameObject[] weaponPrefabs;

        private List<Weapon.WeaponBase> activeWeapons = new List<Weapon.WeaponBase>();

        private void Awake()
        {
            if (weaponContainer == null)
            {
                // 무기 컨테이너가 없으면 새로 생성
                GameObject container = new GameObject("WeaponContainer");
                container.transform.SetParent(transform);
                container.transform.localPosition = Vector3.zero;
                weaponContainer = container.transform;
            }
        }

        public void AddWeapon(GameObject weaponPrefab)
        {
            if (weaponPrefab == null || weaponContainer == null)
                return;

            GameObject weaponObj = Instantiate(weaponPrefab, weaponContainer);
            var weapon = weaponObj.GetComponent<Weapon.WeaponBase>();

            if (weapon != null)
            {
                activeWeapons.Add(weapon);
            }
        }

        public void AddWeaponByIndex(int index)
        {
            if (weaponPrefabs == null || index < 0 || index >= weaponPrefabs.Length)
                return;

            AddWeapon(weaponPrefabs[index]);
        }

        public void UpgradeWeapon(int index)
        {
            if (index < 0 || index >= activeWeapons.Count)
                return;

            activeWeapons[index].UpgradeWeapon();
        }

        public void UpgradeAllWeapons()
        {
            foreach (var weapon in activeWeapons)
            {
                weapon.UpgradeWeapon();
            }
        }

        public int ActiveWeaponCount => activeWeapons.Count;
        public List<Weapon.WeaponBase> ActiveWeapons => activeWeapons;
    }
}
