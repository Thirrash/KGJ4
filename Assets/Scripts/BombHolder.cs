using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHolder : MonoBehaviour
{
    public static BombHolder Instance;
    public List<Bomb> AtomicBombs = new List<Bomb>();

    public void AddAtomicBomb(Bomb b) {
        AtomicBombs.Add(b);
    }

    public void RemoveAtomicBomb(Bomb b) {
        AtomicBombs.Remove(b);
    }

    void Start() {
        Instance = this;
    }

    void Update() {

    }
}
