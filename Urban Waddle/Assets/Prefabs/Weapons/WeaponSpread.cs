using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponSpread : NetworkBehaviour, WeaponBase {

    public GameObject projectile;
    [SerializeField]
    private GameObject mesh;

    [SyncVar]
    private int ammo = 3;

    public float speed = 50.0f;

    weaponType type = weaponType.thrown;

    public weaponType GetWeaponType()
    {
        return type;
    }

    public GameObject GetMesh()
    {
        return mesh;
    }

    public void CmdFire(Vector3 origin, Vector3 dir)
    {
        GameObject bullet = (GameObject)Instantiate(projectile, origin, Quaternion.LookRotation(dir));
        NetworkServer.Spawn(bullet);

        dir = Quaternion.LookRotation(dir).eulerAngles;
        Vector3 mod = new Vector3(0, 7.5f, 0);

        bullet = (GameObject)Instantiate(projectile, origin, Quaternion.Euler(dir + mod));
        NetworkServer.Spawn(bullet);

        mod = new Vector3(0, -7.5f, 0);
        bullet = (GameObject)Instantiate(projectile, origin, Quaternion.Euler(dir + mod));
        NetworkServer.Spawn(bullet);

        mod = new Vector3(0, 15, 0);
        bullet = (GameObject)Instantiate(projectile, origin, Quaternion.Euler(dir + mod));
        NetworkServer.Spawn(bullet);

        mod = new Vector3(0, -15, 0);
        bullet = (GameObject)Instantiate(projectile, origin, Quaternion.Euler(dir + mod));
        NetworkServer.Spawn(bullet);

        ammo--;
    }

    public string GetWeaponName()
    {
        return "Test";
    }

    public bool isEmpty()
    {
        return (ammo < 1);
    }
}
