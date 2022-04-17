<template>
  <div
    class="
      bg-img
      rounded
      elevation-5
      my-2
      d-flex
      align-items-end
      text-light
      selectable
      select-grow
    "
    data-bs-toggle="modal"
    data-bs-target="#active-vault"
    @click="setActive"
  >
    <div class="w-100 p-2">
      <h4 class="m-0 text-shadow">
        {{ vault.name }}
      </h4>
    </div>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      // backgroundImg: computed(() => `url('https://picsum.photos/300')`),
      setActive() {
        AppState.activeVault = props.vault
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-img {
  min-height: 20vh;
  background-blend-mode: darken;
  background-image: v-bind(backgroundImg);
  background-size: cover;
  background-position: center;
}
</style>