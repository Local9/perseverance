<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import Icon from '@iconify/svelte';
  import { setCitizen } from '@store/citizen';
  import type { ICitizen } from '../../@types/citizen';
  import CitizenForm from './CitizenForm.svelte';

  export let citizen: ICitizen;
  export let showNameplate: boolean = true;

  function onClickSetCitizen() {
    setCitizen(citizen);
  }

  function onClickDeleteCitizen() {
    console.log(`delete citizen ${citizen.id}`);
  }

  let showModal: boolean = false;
</script>

{#if showModal}
  <CitizenForm bind:showModal bind:citizen />
{/if}

<article>
  {#if showNameplate}
    <div class="nameplate">
      {#if !citizen?.imageId}
        <Icon icon="game-icons:character" width="80" height="80" />
      {:else}
        <img src={citizen?.imageId} alt="{citizen.name} {citizen.surname}" />
      {/if}
      <div>
        <ul>
          <li>
            <small>SSN: {citizen.socialSecurityNumber}</small>
          </li>
        </ul>
      </div>
    </div>
  {/if}
  <div class="w-96 grid grid-cols-5" role="group">
    <Button class="text-ellipsis col-span-3 inline-block rounded-l h-10 w-64 overflow-hidden whitespace-nowrap" on:click={onClickSetCitizen}>{citizen.name} {citizen.surname}</Button>
    <Button class="inline-block rounded-none w-16" on:click={() => (showModal = true)}>Edit</Button>
    <Button class="inline-block rounded-r w-16 text-center" variant="danger" on:click={onClickDeleteCitizen}>Delete</Button>
  </div>
</article>

<style type="scss">
  article {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin: unset;
  }

  article .nameplate {
    display: flex;
    width: 100%;
    flex-direction: row;
    align-items: center;
  }

  img {
    width: 80px;
    height: 80px;
    border-radius: 50%;
    margin-right: 1rem;
    margin-bottom: 1rem;
  }

  ul {
    list-style: none;
    padding: 0;
    margin: 0;

    li {
      list-style: none;
      margin: 0;
      padding: 0;
    }
  }
</style>
