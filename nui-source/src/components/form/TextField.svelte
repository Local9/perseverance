<script lang="ts">
  import Input from './inputs/Input.svelte';

  export let label: string = '';
  export let optionalLabel: string = '';
  export let value: any = '';
</script>

<div class="{$$restProps.class} relative text-field flex flex-col mb-3">
  {#if label}
    <label for={$$restProps.id} class="mb-1 dark:text-white">
      {label}
      {#if optionalLabel}
        <span class="text-gray-500 dark:text-gray-400"> ({optionalLabel})</span>
      {/if}
    </label>
  {/if}
  <Input bind:value {...$$restProps} />
</div>
