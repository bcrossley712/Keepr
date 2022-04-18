<template>
  <form @submit.prevent="handleSubmit">
    <div class="component">
      <h2>New Keep</h2>
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
import { keepsService } from "../services/KeepsService"
import { Modal } from "bootstrap"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        try {
          await keepsService.createKeep(editable.value)
          Modal.getOrCreateInstance(document.getElementById('new-keep')).hide()
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