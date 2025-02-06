<template>
  <el-container class="layout">
    <el-aside class="patient">
      <el-scrollbar>
        <el-input v-model="query" placeholder="请输入患者编号或姓名" />
        <el-menu v-loading="loading">
          <el-sub-menu v-for="p in patients" :index="p.id">
            <template #title> {{ p.patientId }}-{{ p.name }} </template>
            <el-menu-item-group v-for="t in p.tests" :title="t.category">
              <el-menu-item @click="handleSelect(p, t)" :index="t.id">
                {{ formatDate(t.reportTime) }}
              </el-menu-item>
            </el-menu-item-group>
            <!-- <el-menu-item-group title="角膜共焦显微镜">
              <el-menu-item index="1-3">2025-1-1</el-menu-item>
            </el-menu-item-group> -->
          </el-sub-menu>
        </el-menu>
      </el-scrollbar>
    </el-aside>

    <el-container>
      <el-header style="text-align: left; font-size: 12px; background-color: lightcyan">
        <div class="toolbar">
          <span>检查项目：{{ selectedTest?.category }}</span>
          <span>检查编号：{{ selectedTest?.testId }}</span>
          <span>患者姓名：{{ selectedPatient?.name }}</span>
        </div>
      </el-header>
      <el-main>
        <el-image
          v-for="tf in selectedTest?.testFiles.map(
            (f) => `http://remote.championimage.com.cn:5279/StaticFiles/${f.filePath}`
          )"
          :src="tf"
          height="300px"
          width="400px"
          fit="cover"
        />
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup>
import { ref, watch } from 'vue'
import { onMounted } from 'vue'
import { get, post } from '@/api/utils'
import dayjs from 'dayjs'

const loading = ref(true)

const formatDate = (str) => {
  if (!str) {
    return ''
  }
  const date = dayjs(str)
  // Then specify how you want your dates to be formatted
  return date.format('YYYY-MM-DD')
}

const selectedPatient = ref(null)
const selectedTest = ref(null)

const selectedImageSrc = ref('')

const handleSelect = (p, t) => {
  selectedPatient.value = p
  selectedTest.value = t
  selectedImageSrc.value = `http://remote.championimage.com.cn:5279/StaticFiles/${selectedTest?.value?.reportPath}`
}

const patients = ref()
onMounted(async () => {
  patients.value = await get('Patients')
  loading.value = false
})

const query = ref('')
watch(query, async (newQ) => {
  loading.value = true
  try {
    patients.value = await get(`Patients/${newQ}`)
    console.log(`patients: ${patients.length}`)
  } catch (error) {
    console.log(error)
  } finally {
    loading.value = false
  }
})
</script>

<style scoped>
.layout {
  /* min-height: 800px; */
  height: 100vh;
}

.layout .el-header {
  position: relative;
  color: var(--el-text-color-primary);
}
.layout .patient {
  color: var(--el-text-color-primary);
}

@media (min-width: 0px) and (max-width: 600px) {
  .patient {
    width: 130px;
  }
}

@media (min-width: 600px) and (max-width: 2048px) {
  .patient {
    width: 200px;
  }
}

.layout .toolbar {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}

.layout .toolbar span {
  padding: 5px;
}
</style>
