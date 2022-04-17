<template>
  <div class="container-fluid">
    <div class="row px-md-3 my-4">
      <div class="col-12 px-md-3 mt-4 d-flex align-items-center">
        <img
          :src="profile.picture"
          alt=""
          class="object-fit-cover rounded img-large bg-secondary"
        />
        <div class="ps-md-4 ps-sm-2">
          <h1>{{ profile.name }}</h1>
          <h3 v-if="account.id == route.params.id">
            Vaults: {{ myVaults.length }}
          </h3>
          <h3 v-else>Vaults: {{ vaults.length }}</h3>
          <h3>Keeps: {{ keeps.length }}</h3>
        </div>
      </div>
    </div>
    <h2 class="mb-0 mt-2 px-3">
      Vaults
      <i
        v-if="account.id == route.params.id"
        class="mdi mdi-plus text-primary selectable"
        title="Create Vault"
        data-bs-toggle="modal"
        data-bs-target="#new-vault"
      ></i>
    </h2>
    <div v-if="account.id == route.params.id" class="px-md-3 px-sm-1 masonry">
      <div v-for="v in myVaults" :key="v.id" class="">
        <Vault :vault="v" />
      </div>
    </div>
    <div v-else class="px-md-3 px-sm-1 masonry">
      <div v-for="v in vaults" :key="v.id" class="">
        <Vault :vault="v" />
      </div>
    </div>
    <h2 class="mb-0 mt-2 px-3">
      Keeps
      <i
        v-if="account.id == route.params.id"
        class="mdi mdi-plus text-primary selectable"
        title="Create Keep"
        data-bs-toggle="modal"
        data-bs-target="#new-keep"
      ></i>
    </h2>
    <div class="px-md-3 px-sm-1 masonry">
      <div v-for="k in keeps" :key="k.id" class="">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
  <ModalSmall id="new-vault">
    <template #body><NewVaultForm /></template>
  </ModalSmall>
  <ModalSmall id="new-keep">
    <template #body><NewKeepForm /></template>
  </ModalSmall>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { useRoute } from "vue-router"
import { vaultsService } from "../services/VaultsService"
import { accountService } from "../services/AccountService"
import Modal from "../components/Modal.vue"
export default {
  components: { Modal },
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await accountService.getProfile(route.params.id)
        await keepsService.getProfilesKeeps(route.params.id)
        await vaultsService.getProfilesVaults(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      route,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      myVaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.activeProfile)
    }
  }
}
</script>

<style scoped lang="scss">
img {
  max-width: 100px;
}
.masonry {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
.img-large {
  min-height: 20vh;
  min-width: 20vh;
}
</style>
