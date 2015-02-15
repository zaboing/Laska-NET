using UnityEngine;
using System.Collections;

using Laska;

public class WhiteField : MonoBehaviour
{
    private GameField gameField;

    public int IndexX { get; set; }
    public int IndexY { get; set; }

    public void Start()
    {
        gameField = transform.parent.GetComponent<GameField>();
        UpdateTower();
    }

    public void SetGameField(GameField gameField)
    {
        this.gameField = gameField;
        UpdateTower();
    }

    public void UpdateTower()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        var tower = gameField[IndexX, IndexY];
        if (tower != null && tower.Count > 0)
        {
            var towerObject = gameField.createTower(tower);
            towerObject.transform.parent = transform;
            towerObject.transform.localPosition = new Vector3(0, 0.8f, 0);
        }
    }
}
