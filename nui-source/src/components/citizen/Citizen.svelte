<script lang="ts">
  import Icon from '@iconify/svelte';
  import { setCitizen } from '@store/citizen';
  import type { ICitizen } from '../../@types/citizen';

  export let citizen: ICitizen;
  export let showNameplate: boolean = true;

  function handleClick() {
    setCitizen(citizen);
  }
</script>

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
  <button
    class="rounded-md disabled:opacity-60 disabled:cursor-not-allowed transition-colors"
    on:click={handleClick}>{citizen.name} {citizen.surname}</button
  >
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
