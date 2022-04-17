<template>
  <div class="container-fluid">
    <div class="row h-100">
      <div class="col-md-6 p-0">
        <img :src="activeKeep.img" alt="" class="modal-img rounded" />
      </div>
      <div
        class="col-md-6 d-flex flex-column justify-content-between flex-grow-1"
      >
        <div class="div d-flex justify-content-center">
          <div class="pe-3 d-flex align-items-center">
            <i
              class="mdi mdi-eye text-primary pe-2 fs-5"
              title="How many times viewed"
            ></i>
            <span>{{ activeKeep.views }}</span>
          </div>
          <div class="d-flex align-items-center">
            <i
              class="mdi mdi-alpha-k-box-outline text-primary pe-2 fs-5"
              title="How many times Kept"
            >
            </i>
            <span>
              {{ activeKeep.kept }}
            </span>
          </div>
        </div>
        <div class="text-center">
          <h2>
            {{ activeKeep.name }}
          </h2>
        </div>
        <div class="div d-flex flex-column align-items-center">
          <p class="px-md-4">
            {{ activeKeep.description }} Lorem ipsum dolor sit amet consectetur,
            adipisicing elit. Fuga pariatur architecto maxime quibusdam nihil
            cumque aperiam modi velit non assumenda a accusamus minus numquam
            necessitatibus ducimus dolore qui, deserunt nam.
          </p>
        </div>
        <div class="border-top border-secondary w-100 px-md-5"></div>
        <div class="div d-flex justify-content-between align-items-center">
          <div class="dropdown">
            <button
              class="btn btn-outline-primary dropdown-toggle"
              type="button"
              id="dropdownMenuButton1"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              ADD TO VAULT
            </button>
            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
              <li v-for="v in myVaults" :key="v.id" @click="addToVault(v.id)">
                <a class="dropdown-item selectable">{{ v.name }}</a>
              </li>
            </ul>
          </div>
          <i
            v-if="activeKeep.creator?.id == account.id"
            class="mdi mdi-delete fs-3 text-danger"
            title="Delete Keep"
          ></i>
          <div class="selectable" @click="goTo">
            <img
              :src="activeKeep.creator?.picture"
              alt=""
              class="img-small rounded"
            />
            {{ activeKeep.creator?.name }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
export default {
  setup() {
    const router = useRouter()
    const editable = ref({})
    return {
      router,
      editable,
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      async addKeepToVault(vaultId) {
        try {
          let newVaultKeep = {
            vaultId: vaultId,
            keepId: AppState.activeKeep.id
          }
          await vaultsService.addKeepToVault(newVaultKeep)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      goTo() {
        Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
        router.push({ name: "Profile", params: { id: AppState.activeKeep?.creatorId } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.modal-img {
  height: 500px;
  object-fit: cover;
  position: center;
  width: 100%;
}
.hidden {
  display: none;
}
</style>