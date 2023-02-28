# Perseverance
A C# FiveM Framework built on SnailyCAD

# Development and Contribution

Parts of the NUI (Mainly the `utils` folder) have been copied from https://github.com/project-error/svelte-lua-boilerplate

To develop and test the NUI without running it in game, you can open the `nui-source` folder and run the following commands;
- `npm run dev --open` this will run the NUI in your browser
- `npm run dev:game` to run it in game (you will need to add a `.env.local` file with the location to deploy the files. An example is provided)

For the C# this is using a System Environment Variable for the build configuration `Local` called `FIVEM_SERVER_PATH` this should point to the same location as the one in the `.env.local`, when building the project you can select the `Local` build config and this will deploy the files to your FiveM development server locally on your machine.

You will need to get the ScaleformUI Assets from https://github.com/manups4e/ScaleformUI/releases
These should be put in their own resource folder so restarting the perseverance resource will not cause the Scaleform GFX files to fail.
