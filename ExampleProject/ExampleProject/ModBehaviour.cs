// ******************************************************************
//       /\ /|       @file       ModBehaviour.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2025-10-29 12:44:08
//    *(__\_\        @Copyright  Copyright (c) 2025, Shadowrabbit
// ******************************************************************

namespace ExampleProject;

public class ModBehaviour : Duckov.Modding.ModBehaviour
{
    protected override void OnAfterSetup()
    {
        base.OnAfterSetup();
        ConfigManager.Instance.Init(info.path);
        ConfigManager.Instance.GenerateConfigs();
    }
}