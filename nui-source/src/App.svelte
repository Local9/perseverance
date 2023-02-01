<script lang="ts">
  import "@picocss/pico";
  import "./app.scss";

  import VisibilityProvider from "./providers/VisibilityProvider.svelte";
  import { debugData } from "./utils/debugData";
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
