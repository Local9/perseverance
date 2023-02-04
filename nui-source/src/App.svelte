<script lang="ts">
  import '@picocss/pico';
  import './app.scss';
  import Landing from '@base/Landing.svelte';
  import Tablet from '@tablet/Tablet.svelte';

  import TabletVisibilityProvider from '@providers/TabletVisibilityProvider.svelte';
  import LandingVisibilityProvider from '@providers/LandingVisibilityProvider.svelte';

  import { debugData } from '@utils/debugData';

  debugData([
    {
      action: 'setLandingVisible',
      data: true,
    },
  ]);
</script>

<LandingVisibilityProvider>
  <Landing />
</LandingVisibilityProvider>

<TabletVisibilityProvider>
  <Tablet />
</TabletVisibilityProvider>
