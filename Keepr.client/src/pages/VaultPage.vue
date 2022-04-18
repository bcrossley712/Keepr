<template>
  <div class="container-fluid">
    <div class="row px-md-3 my-4">
      <div
        class="
          col-12
          px-md-3
          mt-4
          d-flex
          align-items-center
          justify-content-between
        "
      >
        <h1 class="m-0">{{ activeVault.name }}</h1>
        <div v-if="activeVault.creatorId == account.id">
          <button class="btn btn-outline-secondary" @click="deleteVault">
            Delete Vault
          </button>
        </div>
      </div>
      <div class="col-12 px-md-3">
        <h5>Keeps: {{ keeps.length }}</h5>
      </div>
    </div>

    <div class="px-md-3 px-sm-1 masonry">
      <div v-for="k in keeps" :key="k.id" class="">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { useRoute, useRouter } from "vue-router"
import { vaultsService } from "../services/VaultsService"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    watchEffect(async () => {
      try {
        if (route.name == 'Vault') {
          await vaultsService.getById(route.params.id)
          await vaultKeepsService.getVaultsKeeps(route.params.id)
        }
      } catch (error) {
        logger.error(error)
        Pop.toast('You cannot enter this vault!', 'error', 'center', 4000)
        router.push({ name: 'Home' })
      }
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      activeVault: computed(() => AppState.activeVault),
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            router.push({ name: "Profile", params: { id: AppState.activeVault.creatorId } })
            await vaultsService.deleteVault(AppState.activeVault.id)
            Pop.toast('Vault Deleted', 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>