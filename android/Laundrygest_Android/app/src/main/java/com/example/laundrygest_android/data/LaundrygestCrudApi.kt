package com.example.laundrygest_android.data

import com.google.gson.GsonBuilder
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.Job
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import kotlin.coroutines.CoroutineContext

class LaundrygestCrudApi : CoroutineScope {
    var job = Job()
    override val coroutineContext: CoroutineContext
        get() = Dispatchers.Main + job

    var urlApi = "http://172.16.24.139:5254/api/"

    private fun getClient(): OkHttpClient {
        var logging = HttpLoggingInterceptor()
        logging.setLevel(HttpLoggingInterceptor.Level.BODY)

        return OkHttpClient.Builder().addInterceptor(logging).build()
    }

    private fun getRetrofit(): Retrofit {
        val gson = GsonBuilder().setLenient().setDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS").create()

        return Retrofit.Builder().baseUrl(urlApi).client(getClient())
            .addConverterFactory(GsonConverterFactory.create(gson)).build()
    }

    fun getClientLogin(user: String, password: String): Client? {
        var client: Client? = null
        runBlocking {
            var response: Response<Client>? = null
            var cor = launch {
                response = getRetrofit()
                    .create(LaundrygestApiService::class.java).getClientLogin(user, password)
            }
            cor.join()
            if (response!!.isSuccessful)
                client = response!!.body()
        }
        return client
    }

    fun getCollectionsClient(code: Int): List<CollectionDto>? {
        var collections: List<CollectionDto>? = null
        runBlocking {
            var response: Response<List<CollectionDto>>? = null
            var cor = launch {
                response = getRetrofit().create(LaundrygestApiService::class.java)
                    .getCollectionsClient(code)
            }
            cor.join()
            if (response!!.isSuccessful)
                collections = response!!.body()
        }
        return collections
    }

}