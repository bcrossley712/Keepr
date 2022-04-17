<template>
  <div class="container-fluid">
    <div class="p-lg-5 p-md-3 p-1 masonry">
      <div v-for="k in keeps" :key="k.id" class="">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import { vaultsService } from "../services/VaultsService";
export default {
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.masonry {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>
