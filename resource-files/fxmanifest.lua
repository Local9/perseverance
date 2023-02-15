game "gta5"
fx_version "cerulean"

author "Local9 <https://github.com/Local9>"
description "curiosity framework"
version "3.0.0"

files {
  "data/**/*",
  "nui-client/**/*",
  "client/Newtonsoft.Json.dll",
  "client/FxEvents.Client.dll",
  "client/ScaleformUI.dll",
}

client_scripts {
  "client/*.net.dll",
  "client-exports/**/*",
}

server_scripts {
  "server/*.net.dll",
}

-- FX NUI SETTINGS
ui_page "nui-client/index.html"
-- Perseverance settings
fxevents_debug_mode "yes"
use_landing_page "true"
enable_hud_landing_page_close "true"
default_player_bucket "0"
