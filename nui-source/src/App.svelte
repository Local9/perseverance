<script lang="ts">
  import VisibilityProvider from "./providers/VisibilityProvider.svelte";
  import Login from "./components/base/Login.svelte";
  import { debugData } from "./utils/debugData";
  import "@picocss/pico";
  import "./app.scss";
  import Tablet from "./components/base/Tablet.svelte";

  debugData([
    {
      action: "setVisible",
      data: true,
    },
  ]);
</script>

<VisibilityProvider>
  <Tablet />
</VisibilityProvider>
