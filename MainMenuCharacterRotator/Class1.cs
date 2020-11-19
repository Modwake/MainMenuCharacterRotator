using UnityEngine;
using Harmony;
using BWModLoader;

namespace MainMenuCharacterRotator
{
    [Mod]
    public class Class1 : MonoBehaviour
    {
        static Transform menuCharacter;

        void Start()
        {
            setMenuCharacter();
        }

        void Update()
        {
            // If it has been found
            if (menuCharacter)
            {
                // Rotate on RMB hold
                if (global::Input.GetMouseButton(1))
                {
                    // Rotation code copied from CharacterCustomizationUI
                    menuCharacter.Rotate(Vector3.up, 1000f * Time.deltaTime * -global::Input.GetAxisRaw("Mouse X"));
                }
            }
        }

        static void setMenuCharacter()
        {
            var musket = GameObject.Find("wpn_standardMusket_LOD1");
            if (musket != null)
            {
                Transform rootTransf = musket.transform.root;
                foreach (Transform transform in rootTransf)
                {
                    if (transform.name == "default_character_rig")
                    {
                        menuCharacter = transform;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(MainMenu), "Start")]
        static class mainMenuStuffPatch
        {
            static void Postfix(MainMenu __instance)
            {
                // Call these so that they set correctly again on returning to the main menu
                setMenuCharacter();
            }
        }
    }
}
