using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MakeAnObject))]
public class MakeAnObjectUI : Editor {

    public int toolBarInt = -1;
    public string[] tests = { "Player", "Ennemies", "Decor", "Item"};

    GameObject targetGameObject;

    PlayerMouvement pm;
    CharacterController controller;

    public CameraController cam;

    public override void OnInspectorGUI()
    {

        if (GameObject.Find(target.name))
        {
            targetGameObject = GameObject.Find(target.name).gameObject;

            GUILayout.Label("Choisissez le type d'objet : ");

            toolBarInt = GUILayout.Toolbar(toolBarInt, tests);

            GUILayout.Space(20);

            if (GUILayout.Button("Remove"))
            {
                RemoveAll();
            }

            if (GUILayout.Button("Remove maker script"))
            {
                DestroyImmediate(targetGameObject.GetComponent<MakeAnObject>());
            }

            switch (toolBarInt)
            {
                case 0:
                    RemoveAll();
                    targetGameObject.tag = "Player";

                    // Instantie le Loader
                    GameObject loader = new GameObject();
                    loader.name = "Loader";
                    loader.tag = "Loader";
                    Loader loadComponent = loader.AddComponent(typeof(Loader)) as Loader;

                    if (targetGameObject.GetComponent<PlayerMouvement>() == null)
                    {
                        pm = targetGameObject.AddComponent(typeof(PlayerMouvement)) as PlayerMouvement;
                        pm.walkSpeed = 12;
                        pm.runSpeed = 24;
                        pm.gravity = 64;
                        pm.rotateSpeed = 150;
                        pm.jumpSpeed = 24;
                        controller = targetGameObject.AddComponent(typeof(CharacterController)) as CharacterController;
                        cam = GameObject.FindWithTag("MainCamera").gameObject.AddComponent(typeof(CameraController)) as CameraController;
                    }
                    break;
                case 1:
                    RemoveAll();
                    // Ajoute touts les scripts du type ennemy;
                    break;
                case 2:
                    RemoveAll();

                    foreach (Component co in targetGameObject.GetComponents<Collider>())
                    {
                        DestroyImmediate(co);
                    }

                    MeshCollider mc = targetGameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
                    break;
                case 3:
                    RemoveAll();

                    foreach (Component co in targetGameObject.GetComponents<Collider>())
                    {
                        DestroyImmediate(co);
                    }
                    if (targetGameObject.GetComponent<ItemObject>() == null)
                    {
                        BoxCollider co = targetGameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
                        ItemObject io = targetGameObject.AddComponent(typeof(ItemObject)) as ItemObject;

                        co.isTrigger = true;
                    }
                    break;
            }
        }
    }

    void RemoveAll()
    {
        RemovePlayerComponents();
        RemoveDecorComponents();
        RemoveEnnemyComponents();
        RemoveItemsComponents();
    }

    void RemovePlayerComponents()
    {

        targetGameObject = GameObject.Find(target.name).gameObject;

        if (GameObject.FindWithTag("Loader"))
        {
            DestroyImmediate(GameObject.FindWithTag("Loader").gameObject);
        }

        if(targetGameObject.GetComponent<PlayerMouvement>() != null)
        {

            toolBarInt = -1;

            targetGameObject.transform.tag = "Untagged";

            pm = targetGameObject.GetComponent<PlayerMouvement>();
            controller = targetGameObject.GetComponent<CharacterController>();

            cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();

            DestroyImmediate(pm);
            DestroyImmediate(cam);
            DestroyImmediate(controller);
            return;
        }
    }

    void RemoveDecorComponents()
    {

        targetGameObject = GameObject.Find(target.name).gameObject;

        if (targetGameObject.GetComponent<MeshCollider>() != null)
        {

            toolBarInt = -1;

            DestroyImmediate(targetGameObject.GetComponent<MeshCollider>());
            return;
        }
    }

    void RemoveEnnemyComponents()
    {
        targetGameObject = GameObject.Find(target.name).gameObject;

        if (targetGameObject.GetComponent<PlayerMouvement>() != null)
        {

            toolBarInt = -1;

            targetGameObject.transform.tag = "Untagged";

            pm = targetGameObject.GetComponent<PlayerMouvement>();
            controller = targetGameObject.GetComponent<CharacterController>();

            cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();

            DestroyImmediate(pm);
            DestroyImmediate(cam);
            DestroyImmediate(controller);
            return;
        }
    }

    void RemoveItemsComponents()
    {

        targetGameObject = GameObject.Find(target.name).gameObject;

        if (targetGameObject.GetComponent<ItemObject>() != null)
        {

            toolBarInt = -1;

            ItemObject io = targetGameObject.GetComponent<ItemObject>();
            Rigidbody rg = targetGameObject.GetComponent<Rigidbody>();

            DestroyImmediate(io);
            DestroyImmediate(rg);
            return;
        }
    }
}
