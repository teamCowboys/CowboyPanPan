using UnityEngine;
using System.Collections;

public interface IPickable {
    
    void Init();

    void checkCollisionWithPlayers();

    void GiveTo(Player player);
}
