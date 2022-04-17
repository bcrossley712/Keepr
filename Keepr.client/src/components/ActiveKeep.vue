<template>
  <div class="component">ActiveKeep</div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
export default {
  setup() {
    return {
      activeKeep: computed(() => AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>