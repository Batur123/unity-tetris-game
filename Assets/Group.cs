using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    float lastFall = 0;

 

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Play.roundVec2(child.position);

            if (!Play.insideBorder(v))
                return false;

            if (Play.grid[(int)v.x, (int)v.y] != null &&
                Play.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        for (int y = 0; y < Play.h; ++y)
            for (int x = 0; x < Play.w; ++x)
                if (Play.grid[x, y] != null)
                    if (Play.grid[x, y].parent == transform)
                        Play.grid[x, y] = null;

        foreach (Transform child in transform)
        {
            Vector2 v = Play.roundVec2(child.position);
            Play.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void Start()
    {
        if (!isValidGridPos())
        {
            Debug.Log("You lost");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(-1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            if (isValidGridPos())
                updateGrid();
            else
                transform.Rotate(0, 0, 90);
        }


        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        { 

            transform.position += new Vector3(0, -1, 0);

            if (isValidGridPos())
            {

                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                Play.deleteFullRows();
                
                FindObjectOfType<SpawnBlock>().SpawnBlocks();

                enabled = false;
            }

            lastFall = Time.time;
        }
    }
}
