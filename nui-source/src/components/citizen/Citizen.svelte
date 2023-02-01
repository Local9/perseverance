<script lang="ts">
  import Icon from "@iconify/svelte";
  import type { ICitizen } from "../../@types/citizen";

  export let citizen: ICitizen;
</script>

<article>
  <div class="nameplate">
    {#if !citizen?.imageId}
      <Icon icon="game-icons:character" width="80" height="80" />
    {:else}
      <img src={citizen?.imageId} alt={citizen.name} />
    {/if}
    <div>
      <ul>
        <li>
          <small>SSN: {citizen.socialSecurityNumber}</small>
        </li>
      </ul>
    </div>
  </div>
  <button>{citizen.name} {citizen.surname}</button>
</article>

<style type="scss">
  article {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin: unset;
    padding: 1rem;
  }

  article .nameplate {
    display: flex;
    width: 100%;
    flex-direction: row;
    align-items: center;
    margin-bottom: 1rem;
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
