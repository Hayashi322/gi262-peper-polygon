using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Student
{
    public class OOPPlayer : Character
    {
        public Inventory inventory;

        public void Start()
        {
            PrintInfo();
            GetRemainEnergy();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector2.up);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2.down);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2.right);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UseFireStorm();
            }
        }

        public void Attack(OOPEnemy _enemy)
        {
            _enemy.TakeDamage(AttackPoint);
        }

        protected override void CheckDead()
        {
            base.CheckDead();
            if (energy <= 0)
            {
                Debug.Log("Player is Dead");
            }
        }

        public void UseFireStorm()
        {
            if (inventory.numberOfItem("FireStorm") > 0)
            {
                inventory.UseItem("FireStorm");
                OOPEnemy[] enemies = SortEnemiesByRemainningEnergy1();
                int count = 3;
                if (count > enemies.Length)
                {
                    count = enemies.Length;
                }
                for (int i = 0; i < count; i++)
                {
                    enemies[i].TakeDamage(10);
                }
            }
            else
            {
                Debug.Log("No FireStorm in inventory");
            }
        }

        public OOPEnemy[] SortEnemiesByRemainningEnergy1()
        {
            var enemies = mapGenerator.GetEnemies();

            // 1. เรียงลำดับ enemies จากน้อยไปมากตาม energy ด้วย Bubble Sort
            // ...
            // ...

            return enemies;
        }

        public OOPEnemy[] SortEnemiesByRemainningEnergy2()
        {
            var enemies = mapGenerator.GetEnemies();

            // 2. เรียงลำดับ enemies จากน้อยไปมากตาม energy ด้วย Sort function ของ C#
            // ...
            // ...

            return enemies;
        }

    }

}