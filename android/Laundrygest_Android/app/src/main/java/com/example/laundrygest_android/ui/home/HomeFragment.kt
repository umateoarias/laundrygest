package com.example.laundrygest_android.ui.home

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.SearchView
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.laundrygest_android.data.LaundrygestCrudApi
import com.example.laundrygest_android.data.PricelistDTO
import com.example.laundrygest_android.databinding.FragmentHomeBinding
import com.example.laundrygest_android.pricelistRCV_adapter

class HomeFragment : Fragment() {

    private var _binding: FragmentHomeBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!
    private var fullList : List<PricelistDTO>? = null
    lateinit var adapter : pricelistRCV_adapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val homeViewModel =
            ViewModelProvider(this).get(HomeViewModel::class.java)

        _binding = FragmentHomeBinding.inflate(inflater, container, false)
        val root: View = binding.root

        var crudApi = LaundrygestCrudApi()
        fullList = crudApi.getPricelists()

        if(fullList!=null) {
            adapter = pricelistRCV_adapter(fullList!!)
            binding.pricelistRecyclerView.layoutManager = LinearLayoutManager(this.context)
            binding.pricelistRecyclerView.adapter = adapter

            binding.searchView.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
                override fun onQueryTextSubmit(query: String?): Boolean = false
                override fun onQueryTextChange(newText: String?): Boolean {
                    filterList(newText.orEmpty())
                    return true
                }
            })
        }
        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    private fun filterList(query: String) {
        val filtered = fullList!!.filter {
            it.name.contains(query, ignoreCase = true)
        }
    }
}