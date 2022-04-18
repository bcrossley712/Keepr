<template>
  <form @submit.prevent="handleSubmit">
    <div class="component">
      <h2>New Vault</h2>
      <div class="mb-3 w-100">
        <label for="name" class="form-label text-secondary">Title:</label>
        <input
          type="text"
          class="form-control"
          name="name"
          id="name"
          placeholder="Title..."
          v-model="editable.name"
        />
      </div>
      <div class="mb-3 w-100">
        <label for="img" class="form-label text-secondary">ImgUrl:</label>
        <input
          type="text"
          class="form-control"
          name="img"
          id="img"
          placeholder="ImgUrl..."
          v-model="editable.img"
        />
      </div>
      <div class="mb-3">
        <label for="description" class="form-label">Description:</label>
        <textarea
          class="form-control"
          name="description"
          id="description"
          rows="6"
          placeholder="Description..."
          v-model="editable.description"
        ></textarea>
      </div>
      <div class="form-check">
        <input
          type="checkbox"
          class="form-check-input"
          name="isPrivate"
          id="isPrivate"
          value="true"
          v-model="editable.isPrivate"
        />
        <label class="form-check-label text-secondary" for="isPrivate">
          Private?
        </label>
        <p class="form-text text-muted">
          Private Vaults can only be seen by you
        </p>
      </div>
      <div class="text-end">
        <button class="btn btn-primary text-dark text-end">Create</button>
      </div>
    </div>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { vaultsService } from "../services/VaultsService"
import { Modal } from "bootstrap"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        try {
          await vaultsService.createVault(editable.value)
          Modal.getOrCreateInstance(document.getElementById('new-vault')).hide()
          editable.value = {}
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
</style>