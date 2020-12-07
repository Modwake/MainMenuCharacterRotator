using UnityEngine;
using Harmony;
using BWModLoader;

namespace MainMenuCharacterRotator
{
    [Mod]
    public class Class1 : MonoBehaviour
    {
        // Stores the character in it
        static Transform menuCharacter;
        void Start()
        {
            //Get the Main Menu customization character
            setMenuCharacter();

            // Setup Harmony for patching into MainMenu.Start()
            HarmonyInstance harmony = HarmonyInstance.Create("com.github.archie");
            harmony.PatchAll();
        }

        void Update()
        {
            // If it has been found
            if (!óèïòòåææäêï.åìçæçìíäåóë.activeSelf && global::Input.GetMouseButton(1) && menuCharacter)
            {
                // Rotation code copied from CharacterCustomizationUI
                menuCharacter.Rotate(Vector3.up, 1000f * Time.deltaTime * -global::Input.GetAxisRaw("Mouse X"));

            }
        }

        static void setMenuCharacter()
        {
            // Find the musket object
            var musket = GameObject.Find("wpn_standardMusket_LOD1");
            if (musket != null)
            {
                // If it exists, then go to root and find the character model in the heirachy
                Transform rootTransf = musket.transform.root;
                foreach (Transform transform in rootTransf)
                {
                    if (transform.name == "default_character_rig")
                    {
                        // Save it for the rotating in Update()
                        menuCharacter = transform;
                        break;
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
