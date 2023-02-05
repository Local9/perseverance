<script lang="ts">
  import '@picocss/pico';
  import './app.scss';
  import Landing from '@base/Landing.svelte';
  import Tablet from '@tablet/Tablet.svelte';

  import { SvelteToast } from '@zerodevx/svelte-toast';

  import TabletVisibilityProvider from '@providers/TabletVisibilityProvider.svelte';
  import LandingVisibilityProvider from '@providers/LandingVisibilityProvider.svelte';

  import { debugData } from '@utils/debugData';

  const options = {
    duration: 4000, // duration of progress bar tween to the `next` value
    initial: 1, // initial progress bar value
    next: 0, // next progress value
    pausable: false, // pause progress bar tween on mouse hover
    dismissable: false, // allow dismiss with close button
    reversed: false, // insert new toast to bottom of stack
    intro: { x: 256 }, // toast intro fly animation settings
    theme: {}, // css var overrides
    classes: [], // user-defined classes
  };

  debugData([
    {
      action: 'setLandingVisible',
      data: true,
    },
  ]);
</script>

<SvelteToast {options} />

<LandingVisibilityProvider>
  <Landing />
</LandingVisibilityProvider>

<TabletVisibilityProvider>
  <Tablet />
</TabletVisibilityProvider>
